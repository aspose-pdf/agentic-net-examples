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

        // Create a configuration with all default sanitization options enabled
        HiddenDataSanitizationOptions options = HiddenDataSanitizationOptions.All();

        // Manually enable page rasterization (convert pages to images) and set DPI for the rasterized pages
        options.ConvertPagesToImages = true;
        options.ImageDpi = 300;

        // Instantiate the sanitizer with the configured options
        HiddenDataSanitizer sanitizer = new HiddenDataSanitizer(options);

        // Load the PDF, apply sanitization, and save the result
        using (Document doc = new Document(inputPath))
        {
            sanitizer.Sanitize(doc);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}