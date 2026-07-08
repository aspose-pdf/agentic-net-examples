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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion to rasterize each page as an image
            var htmlOpts = new HtmlSaveOptions
            {
                // Rasterize pages – each page will be saved as an external PNG file referenced via SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Set DPI for rasterization
                ImageResolution = 150,
                // Keep a single HTML file (optional, but common when rasterizing pages)
                SplitIntoPages = false,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Save the PDF as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML saved to '{outputHtml}'.");
    }
}