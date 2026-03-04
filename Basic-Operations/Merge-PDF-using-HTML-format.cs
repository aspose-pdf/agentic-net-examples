using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output HTML file
        const string outputHtml = "merged.html";

        // Verify that all input files exist
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        try
        {
            // Create an empty document that will hold the merged result
            using (Document mergedDoc = new Document())
            {
                // Merge the PDF files into the empty document
                mergedDoc.Merge(pdfFiles);

                // Prepare HTML save options (explicitly required for non‑PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle possible platform issues
                try
                {
                    mergedDoc.Save(outputHtml, htmlOpts);
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