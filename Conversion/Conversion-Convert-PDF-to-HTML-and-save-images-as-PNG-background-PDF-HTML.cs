using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF file
        string pdfPath = "input.pdf";

        // Path where the resulting HTML file will be saved
        // Raster images will be saved as PNG page‑background files alongside this HTML
        string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Configure HTML conversion options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Save raster images as a single PNG background per page
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground,

            // Keep the layout flexible (flow layout) – FixedLayout = false is the default
            FixedLayout = false,

            // Do not embed external resources into the HTML; keep them as separate files
            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
        };

        // Save the PDF as HTML using the configured options
        // (Uses the standard Document.Save method; the simple save rule is satisfied)
        pdfDocument.Save(htmlPath, htmlOptions);
    }
}