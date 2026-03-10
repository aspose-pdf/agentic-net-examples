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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create custom HtmlSaveOptions and configure desired settings
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (fonts, images, CSS) into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    
                    // Save raster images as PNG data embedded within SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    
                    // Optional: generate separate HTML files per PDF page
                    // SplitIntoPages = true,
                    
                    // Set the HTML page title based on the output file name
                    Title = Path.GetFileNameWithoutExtension(outputHtml)
                };

                // Save the PDF as HTML using the custom options
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtml}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}