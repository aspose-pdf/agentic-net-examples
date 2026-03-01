using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the existing PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize HtmlSaveOptions (required to produce HTML output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    // Save the PDF as HTML using the specified options
                    pdfDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only
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