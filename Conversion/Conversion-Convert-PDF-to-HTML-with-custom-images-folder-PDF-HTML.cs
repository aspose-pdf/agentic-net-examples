using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesFolder   = "html_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save all raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Specify the custom folder where images will be written
                    SpecialFolderForAllImages = imagesFolder,
                    // Optional: embed resources into a single HTML file (set to false to keep separate files)
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion requires GDI+ on Windows; handle non‑Windows platforms gracefully
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}