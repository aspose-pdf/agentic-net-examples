using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, HtmlSaveOptions, etc.

class PdfToHtmlConverter
{
    static void Main()
    {
        // Paths – adjust as needed.
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesFolder   = "HtmlImages"; // Folder where extracted images will be saved.

        // Validate input file.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images folder exists.
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Save all extracted images into the specified folder.
                    SpecialFolderForAllImages = imagesFolder,

                    // Example: store raster images as external PNG files referenced via SVG.
                    // Adjust as required (e.g., AsPngImagesEmbeddedIntoSvg, AsEmbeddedPartsOfPngPageBackground, DontSave).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Perform the conversion.
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            Console.WriteLine($"Extracted images saved in folder: {imagesFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}