using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SanitizedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ---------- Hidden data sanitization ----------
                // Use the modern HiddenDataSanitizer API (Aspose.Pdf.Security.HiddenDataSanitization)
                var sanitizationOptions = new HiddenDataSanitizationOptions();
                // Options can be tweaked here, e.g. sanitizationOptions.RemoveMetadata = false;
                var sanitizer = new HiddenDataSanitizer(sanitizationOptions);
                sanitizer.Sanitize(doc);

                // ---------- Convert each page to an image ----------
                // PngDevice (or any other ImageDevice) is the recommended way to render pages to images.
                var resolution = new Resolution(300); // 300 DPI – adjust as needed
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    var pngDevice = new PngDevice(resolution);
                    string outPath = Path.Combine(outputDir, $"page_{i}.png");
                    pngDevice.Process(doc.Pages[i], outPath);
                }
            }

            Console.WriteLine($"Sanitized images have been saved to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
