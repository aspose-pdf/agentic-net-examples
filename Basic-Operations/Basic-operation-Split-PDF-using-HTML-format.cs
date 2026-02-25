using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file path (base name; multiple files will be created when splitting)
        const string htmlOutput = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions options = new HtmlSaveOptions
                {
                    // Split each PDF page into a separate HTML file
                    SplitIntoPages = true,
                    // Embed images as PNGs inside SVG to preserve quality
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ and is Windows‑only.
                // Wrap the save call in a try‑catch to handle non‑Windows platforms gracefully.
                try
                {
                    doc.Save(htmlOutput, options);
                    Console.WriteLine("PDF successfully split into HTML pages.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}