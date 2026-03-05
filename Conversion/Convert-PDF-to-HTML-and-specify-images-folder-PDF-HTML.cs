using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output HTML and the folder where images will be stored
        // Use full paths relative to the executable directory to avoid "File not found" errors.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputPdfPath   = Path.Combine(baseDir, "input.pdf");
        string outputHtmlPath = Path.Combine(baseDir, "output.html");
        string imagesFolder   = Path.Combine(baseDir, "Images");

        // Verify that the source PDF exists before attempting conversion.
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{inputPdfPath}'. Please ensure the file exists.");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Specify the folder where all extracted images will be saved
            htmlOptions.SpecialFolderForAllImages = imagesFolder;

            // Choose how raster images are saved (optional, but demonstrates usage)
            // This will save each raster image as an external PNG file referenced via an SVG wrapper
            htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg;

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML. Images saved in folder: '{imagesFolder}'");
    }
}
