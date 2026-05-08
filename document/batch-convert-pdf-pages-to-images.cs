using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImagesDir = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputImagesDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure sanitization options: we want to convert pages to images
            var sanitizationOptions = new HiddenDataSanitizationOptions
            {
                ConvertPagesToImages = true,   // enable page‑to‑image conversion
                ImageDpi = 150                 // DPI for the generated images
            };

            // Create the sanitizer with the configured options
            var sanitizer = new HiddenDataSanitizer(sanitizationOptions);

            // Perform hidden‑data sanitization (removes metadata, etc.)
            sanitizer.Sanitize(pdfDoc);

            // Convert all pages to separate image files.
            // The static method writes the images to the current working directory,
            // so temporarily change the working directory to the desired output folder.
            string originalWorkingDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(outputImagesDir);

            // DPI is taken from the options; the method returns void.
            HiddenDataSanitizer.SanitizeAllToImages(pdfDoc, sanitizationOptions.ImageDpi);

            // Restore the original working directory
            Directory.SetCurrentDirectory(originalWorkingDir);
        }

        Console.WriteLine($"All pages have been converted to images in folder: {outputImagesDir}");
    }
}