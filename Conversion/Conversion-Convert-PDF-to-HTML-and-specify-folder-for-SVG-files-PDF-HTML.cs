using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgFolderPath  = "svg_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted SVG images exists
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Folder where SVG images will be saved
                    SpecialFolderForSvgImages = svgFolderPath,
                    // Example: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Perform the conversion
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                Console.WriteLine($"SVG images saved to folder: {svgFolderPath}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}