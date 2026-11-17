using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing ScreenAnnotation(s)
        const string outputDir = "ExtractedMedia";    // Folder to store extracted media files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                int mediaIndex = 0;

                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Examine each annotation on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only ScreenAnnotation objects (they can contain media)
                        if (annot is ScreenAnnotation screenAnnot)
                        {
                            // Use reflection to access RichMediaContent hierarchy – it may not exist in older versions
                            object richMedia = GetPropertyValue(screenAnnot, "RichMediaContent");
                            if (richMedia == null) continue;

                            object settings = GetPropertyValue(richMedia, "RichMediaSettings");
                            if (settings == null) continue;

                            object configuration = GetPropertyValue(settings, "RichMediaConfiguration");
                            if (configuration == null) continue;

                            var instances = GetPropertyValue(configuration, "RichMediaInstances") as IEnumerable;
                            if (instances == null) continue;

                            foreach (object instance in instances)
                            {
                                object fileSpec = GetPropertyValue(instance, "File");
                                if (fileSpec == null) continue;

                                object embedded = GetPropertyValue(fileSpec, "EmbeddedFile");
                                if (embedded == null) continue;

                                // Determine a file name (preserve original extension if available)
                                string originalName = GetPropertyValue(fileSpec, "Name") as string ?? string.Empty;
                                string extension = Path.GetExtension(originalName);
                                if (string.IsNullOrEmpty(extension))
                                    extension = ".bin";

                                string outPath = Path.Combine(outputDir, $"media_{mediaIndex}{extension}");

                                // Save the embedded media to disk via reflection (EmbeddedFile.Save(string))
                                var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                                saveMethod?.Invoke(embedded, new object[] { outPath });

                                Console.WriteLine($"Extracted media saved to: {outPath}");
                                mediaIndex++;
                            }
                        }
                    }
                }

                if (mediaIndex == 0)
                    Console.WriteLine("No media found in ScreenAnnotation objects.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper that safely gets a property value via reflection; returns null if not found or on error.
    private static object GetPropertyValue(object obj, string propertyName)
    {
        if (obj == null) return null;
        var prop = obj.GetType().GetProperty(propertyName);
        return prop?.GetValue(obj);
    }
}
