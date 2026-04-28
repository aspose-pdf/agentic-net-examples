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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define the resolution for the output TIFF (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,   // Use LZW compression
                Depth = ColorDepth.Default,          // Default color depth
                Shape = ShapeType.Landscape,         // Landscape orientation
                SkipBlankPages = false               // Do not skip blank pages
            };

            // Create a TiffDevice with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page (page 1) to a single‑page TIFF file
            tiffDevice.Process(pdfDoc, 1, 1, outputPath);
        }

        Console.WriteLine($"Single‑page TIFF saved to '{outputPath}'.");
    }
}