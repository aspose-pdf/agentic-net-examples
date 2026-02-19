using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfToHtml
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string pdfPath = "input.pdf";

        // Output directory where individual HTML pages will be saved
        const string outputDir = "output_html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Create one HTML file per PDF page
                SplitIntoPages = true,

                // Embed all referenced resources (CSS, images, fonts) into each HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Save raster images as PNG embedded into SVG (base‑64 encoded)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Perform the conversion; each page will be saved as a separate HTML file
            pdfDocument.Save(outputDir, htmlOptions);

            Console.WriteLine($"PDF successfully split into HTML pages in folder '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
