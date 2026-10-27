using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_navigable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and ensure deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ---------- Sanitization ----------
                // Remove all annotations (links, comments, etc.)
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }

                // Remove any JavaScript actions attached to the document
                if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
                {
                    // JavaScriptCollection does not expose Count directly; iterate via Keys
                    var keys = doc.JavaScript.Keys.ToList();
                    foreach (var key in keys)
                    {
                        doc.JavaScript.Remove(key);
                    }
                }

                // Remove embedded files (if any) – use Delete by name because Clear() does not exist
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // ---------- Accessibility & Navigation ----------
                // Enable automatic tagging (detects headings based on font size, etc.)
                AutoTaggingSettings.Default.EnableAutoTagging = true;

                // Set language and title for the tagged PDF
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Obtain the root of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a simple heading for each page to build a navigable outline
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    HeaderElement header = taggedContent.CreateHeaderElement(1);
                    header.SetText($"Page {i}");
                    header.Language = "en-US";
                    root.AppendChild(header);
                }

                // Save the cleaned, tagged PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Clean, navigable PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
