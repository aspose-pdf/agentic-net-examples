using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
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
            // Load the first PDF as the target document
            using (Document target = new Document(pdfFiles[0]))
            {
                // Append the remaining PDFs to the target
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    using (Document source = new Document(pdfFiles[i]))
                    {
                        target.Pages.Add(source.Pages);
                    }
                }

                // Configure HTML save options (required for HTML output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNGs embedded in SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ (Windows only); handle cross‑platform scenarios
                try
                {
                    target.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"Merged HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
                catch (DllNotFoundException)
                {
                    Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}