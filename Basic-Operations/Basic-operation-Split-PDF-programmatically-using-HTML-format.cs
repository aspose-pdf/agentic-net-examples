using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file (base name; multiple files will be created when splitting)
        const string htmlOutput = "output.html";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions opts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNGs inside SVG to keep them self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion requires GDI+ and is Windows‑only; handle possible platform issues
                try
                {
                    doc.Save(htmlOutput, opts);
                    Console.WriteLine("PDF successfully split into individual HTML pages.");
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