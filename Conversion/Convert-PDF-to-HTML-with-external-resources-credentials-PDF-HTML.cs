using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToHtmlConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example: embed all resources (images, CSS) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Example: save raster images as PNG wrapped in SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}