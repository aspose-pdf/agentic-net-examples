using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable global auto‑tagging; it will create heading structure after cleaning
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        using (Document doc = new Document(inputPath))
        {
            // ---- Remove hidden data manually ----
            // 1. Clear document information (metadata)
            doc.Info.Title = null;
            doc.Info.Author = null;
            doc.Info.Subject = null;
            doc.Info.Keywords = null;
            doc.Info.Creator = null;
            doc.Info.Producer = null;
            doc.Info.ModDate = DateTime.Now; // optional – set a new modification date

            // 2. XMP metadata – left unchanged (requires SaveOptions to strip)

            // 3. Remove annotations from every page
            foreach (Page page in doc.Pages)
                page.Annotations?.Clear();

            // 4. Remove embedded files (attachments)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                }
            }

            // 5. Remove JavaScript actions
            doc.OpenAction = null;

            // 6. Remove form fields (if any)
            if (doc.Form != null && doc.Form.Count > 0)
            {
                for (int i = doc.Form.Count; i >= 1; i--)
                {
                    var field = doc.Form[i];
                    if (field != null && !string.IsNullOrEmpty(field.FullName))
                        doc.Form.Delete(field.FullName);
                }
            }

            // ---- Re‑apply tagging (including headings) ----
            // Ensure the document is marked as tagged (reflection works across versions)
            if (doc.TaggedContent != null)
            {
                var tagged = doc.TaggedContent;
                var setTaggedMethod = tagged.GetType().GetMethod("SetTagged");
                if (setTaggedMethod != null)
                {
                    setTaggedMethod.Invoke(tagged, new object[] { true });
                }
                else
                {
                    var isTaggedProp = tagged.GetType().GetProperty("IsTagged");
                    if (isTaggedProp != null && isTaggedProp.CanWrite)
                    {
                        isTaggedProp.SetValue(tagged, true);
                    }
                }
            }

            // Set language for accessibility (optional)
            doc.TaggedContent?.SetLanguage("en-US");

            // Save the cleaned and re‑tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
