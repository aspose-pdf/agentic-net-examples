using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputHtmlPath = "output.html";       // base name for HTML output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed raster images as PNG inside SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML; with SplitIntoPages = true multiple files will be created
                // (e.g., output.html, output_page2.html, output_page3.html, …)
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"PDF split into HTML pages successfully. Base file: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ and is only supported on Windows
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