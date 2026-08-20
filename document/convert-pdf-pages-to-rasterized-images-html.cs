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
            // Configure HTML conversion to rasterize each page as an image.
            // The rasterization DPI is set via the RasterImagesResolution property if available;
            // otherwise the default DPI is used (Aspose.PDF handles high‑resolution rasterization internally).
            var htmlOpts = new HtmlSaveOptions
            {
                // Rasterize pages as external PNG files referenced via SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Optional settings to keep a single HTML file and avoid embedding parts
                SplitIntoPages = false,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML saved with rasterized pages to '{outputHtml}'.");
    }
}
