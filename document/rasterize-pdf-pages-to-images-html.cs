using System;
using System.IO;
using Aspose.Pdf;

namespace PdfConversionDemo
{
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
                    // Configure HTML conversion to rasterize each page as an image
                    HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                    {
                        // Rasterize pages – each page will be saved as an external PNG referenced via SVG
                        RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                        // Optional: keep all pages in a single HTML file
                        SplitIntoPages = false,
                        // Optional: do not embed the raster images into the HTML (they are external files)
                        PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
                    };

                    // Save the document as HTML using the configured options
                    pdfDoc.Save(outputHtml, htmlOpts);
                }

                Console.WriteLine($"Conversion completed: {outputHtml}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
