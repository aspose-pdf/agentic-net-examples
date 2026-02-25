using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string htmlOutput = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions opts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed images as PNGs inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion requires GDI+; handle platforms where it is unavailable
                try
                {
                    doc.Save(htmlOutput, opts);
                    Console.WriteLine($"PDF split into HTML pages saved to '{htmlOutput}' (multiple files created).");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}