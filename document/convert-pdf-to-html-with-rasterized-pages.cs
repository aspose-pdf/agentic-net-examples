using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure HTML save options to rasterize each PDF page as an image.
            // The ConvertPagesToImages and ImageDpi properties were removed in newer
            // Aspose.PDF versions. Rasterization is now controlled via the
            // RasterImagesSavingMode property.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Save each page as an external PNG image referenced through SVG.
                // This forces rasterization of the page content.
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Optional: keep the whole document in a single HTML file.
                SplitIntoPages = false,
                // Optional: do not embed the raster images (they are saved as external files).
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with rasterized pages: {outputPath}");
    }
}
