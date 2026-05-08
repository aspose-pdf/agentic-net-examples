using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, HtmlSaveOptions)

class Program
{
    static void Main()
    {
        // Input PDF (generated from XML) and output HTML paths
        const string pdfPath  = "input.pdf";
        const string htmlPath = "preview.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize HTML save options (required for non‑PDF output)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Example: embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Optional: choose raster image handling mode
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the PDF as HTML for web preview
            pdfDoc.Save(htmlPath, htmlOpts);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
    }
}