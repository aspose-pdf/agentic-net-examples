using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML save options to rasterize each PDF page as an image
                var htmlOpts = new HtmlSaveOptions
                {
                    // Rasterize each page (PNG files referenced via SVG)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Keep a single HTML file (optional, adjust as needed)
                    SplitIntoPages = false,
                    // Do not embed parts; keep images external
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle possible platform issues
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved with rasterized pages to '{outputHtml}'.");
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
