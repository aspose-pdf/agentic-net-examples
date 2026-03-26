using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            // Rasterize each PDF page to an image (150 DPI) and embed the images in a single HTML file.
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                // Save rasterized page images as external PNG files referenced via SVG (most compatible mode).
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // DPI (resolution) for the rasterized images.
                ImageResolution = 150,
                // Embed all other resources (CSS, fonts, etc.) into the HTML so the result is a single file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Keep the output as a single HTML document rather than one file per page.
                SplitIntoPages = false
            };

            pdfDocument.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to HTML with rasterized pages: {outputPath}");
    }
}
