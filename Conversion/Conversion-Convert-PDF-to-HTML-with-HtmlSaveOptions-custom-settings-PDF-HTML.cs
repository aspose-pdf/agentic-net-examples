using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure custom HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all generated resources (CSS, images, etc.) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Store raster images as PNG wrapped in SVG for better compatibility
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Optional: give the resulting HTML a meaningful title
                    Title = "Converted HTML Document"
                    
                    // Optional: uncomment to generate one HTML file per PDF page
                    // SplitIntoPages = true
                };

                // Save the PDF as HTML using the custom options
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}