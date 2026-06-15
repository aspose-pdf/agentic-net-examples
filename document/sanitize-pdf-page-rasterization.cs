using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a HiddenDataSanitizationOptions instance with all default options enabled
            HiddenDataSanitizationOptions options = HiddenDataSanitizationOptions.All();

            // Manually enable page rasterization (convert pages to images) for selective conversion
            // This will rasterize the page content, removing hidden data while preserving visual appearance
            options.ConvertPagesToImages = true;

            // Optionally adjust image DPI for the rasterized pages
            options.ImageDpi = 150; // 150 DPI resolution

            // Create the sanitizer with the configured options
            HiddenDataSanitizer sanitizer = new HiddenDataSanitizer(options);

            // Perform the sanitization on the loaded document
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}