using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesFolder   = "html_images";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images folder exists (it will be used by HtmlSaveOptions)
        try
        {
            Directory.CreateDirectory(imagesFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create images folder '{imagesFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save all images into the specified folder
                    SpecialFolderForAllImages = imagesFolder,

                    // Optional: embed images as PNG inside SVG (you can change as needed)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the configured options
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
            Console.WriteLine($"All images saved to folder: '{imagesFolder}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}