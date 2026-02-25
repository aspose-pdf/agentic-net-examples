using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgFolderPath  = "svg_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the custom folder for SVG images exists
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save all generated SVG images into the custom folder
                    SpecialFolderForSvgImages = svgFolderPath,

                    // Example setting: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the options (extension alone is not enough)
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            Console.WriteLine($"SVG images stored in: {svgFolderPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}