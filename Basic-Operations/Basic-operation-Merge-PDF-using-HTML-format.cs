using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (HtmlSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] pdfFiles = { "first.pdf", "second.pdf" };
        const string outputHtml = "merged.html";

        // Verify that all input files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        try
        {
            // Use nested using blocks for deterministic disposal (rule: document-disposal-with-using)
            using (Document target = new Document(pdfFiles[0]))
            using (Document source = new Document(pdfFiles[1]))
            {
                // Merge the source pages into the target document
                target.Pages.Add(source.Pages);

                // Prepare HTML save options (rule: save-to-non-pdf-always-use-save-options)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed raster images as PNGs inside SVG (common choice)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch for cross‑platform safety.
                try
                {
                    target.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"Merged HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}