using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "first_page.tif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define the resolution for the TIFF image (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,          // Use LZW compression
                Depth = ColorDepth.Format8bpp,              // 8 bits per pixel
                Shape = ShapeType.Portrait,                 // Portrait orientation
                SkipBlankPages = false                      // Do not skip blank pages
            };

            // Initialize the TiffDevice with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Process only the first page (pages are 1‑based) and write to a TIFF file
            using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
            {
                tiffDevice.Process(pdfDocument, 1, 1, tiffStream);
            }
        }

        Console.WriteLine($"TIFF created: {outputPath}");
    }
}