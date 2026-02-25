using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions is defined in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesFolder   = "Images"; // folder where all images will be saved

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save all raster images into the specified folder
                    SpecialFolderForAllImages = imagesFolder,
                    // Example: embed images as PNGs wrapped in SVG (optional)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Perform the conversion
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                Console.WriteLine($"All images saved to folder: {imagesFolder}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}