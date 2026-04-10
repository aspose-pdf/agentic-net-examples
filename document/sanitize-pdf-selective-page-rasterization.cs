using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a sanitization options object with all default options enabled
            HiddenDataSanitizationOptions options = HiddenDataSanitizationOptions.All();

            // Enable rasterization (conversion of pages to images) for selective conversion
            options.ConvertPagesToImages = true;
            // Optional: set the DPI for the rasterized images
            options.ImageDpi = 300;

            // Instantiate the sanitizer with the configured options
            HiddenDataSanitizer sanitizer = new HiddenDataSanitizer(options);

            // Define which pages should be rasterized (e.g., pages 2 and 4)
            int[] pagesToRasterize = { 2, 4 };

            // Rasterize the selected pages using the static method
            foreach (int pageNumber in pagesToRasterize)
            {
                // Static call – do not use an instance reference
                HiddenDataSanitizer.SanitizeAllToImages(doc, pageNumber);
            }

            // Apply full sanitization to the document (optional, based on requirements)
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}