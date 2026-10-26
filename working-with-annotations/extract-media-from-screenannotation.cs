using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedMedia";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document doc = new Document(inputPath))
            {
                int mediaCount = 0;

                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Examine each annotation on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only ScreenAnnotation objects
                        if (annot is ScreenAnnotation screenAnnot)
                        {
                            // Use reflection to access RichMediaContent (may not exist in older versions)
                            PropertyInfo richMediaProp = screenAnnot.GetType().GetProperty("RichMediaContent");
                            if (richMediaProp == null)
                                continue; // RichMediaContent not supported – skip

                            object richMedia = richMediaProp.GetValue(screenAnnot);
                            if (richMedia == null)
                                continue;

                            // Get the EmbeddedFile property from RichMediaContent
                            PropertyInfo embeddedFileProp = richMedia.GetType().GetProperty("EmbeddedFile");
                            if (embeddedFileProp == null)
                                continue;

                            object embeddedFile = embeddedFileProp.GetValue(richMedia);
                            if (embeddedFile == null)
                                continue;

                            // Determine a file name/extension
                            string fileName = string.Empty;
                            PropertyInfo fileNameProp = embeddedFile.GetType().GetProperty("FileName");
                            if (fileNameProp != null)
                            {
                                object fn = fileNameProp.GetValue(embeddedFile);
                                if (fn != null)
                                    fileName = fn.ToString();
                            }

                            string ext = Path.GetExtension(fileName);
                            if (string.IsNullOrEmpty(ext))
                                ext = ".bin";

                            mediaCount++;
                            string outPath = Path.Combine(outputDir, $"media_{mediaCount}{ext}");

                            // Call EmbeddedFile.Save(string)
                            MethodInfo saveMethod = embeddedFile.GetType().GetMethod("Save", new[] { typeof(string) });
                            if (saveMethod != null)
                            {
                                saveMethod.Invoke(embeddedFile, new object[] { outPath });
                                Console.WriteLine($"Saved media from page {i} to '{outPath}'.");
                            }
                        }
                    }
                }

                if (mediaCount == 0)
                    Console.WriteLine("No ScreenAnnotation with embedded media found.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
