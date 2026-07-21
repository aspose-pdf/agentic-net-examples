using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputHtml = "output.html";        // base name for HTML pages

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options – enable one HTML file per PDF page
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed raster images as PNG inside SVG (Windows‑only GDI+ requirement)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document; multiple HTML files will be generated (output_1.html, output_2.html, …)
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine("PDF successfully converted to multi‑page HTML.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion uses GDI+ and is only supported on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}