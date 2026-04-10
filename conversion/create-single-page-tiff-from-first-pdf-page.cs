using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define resolution (300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,
                Depth = ColorDepth.Format8bpp,
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create TIFF device with resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page to a single‑page TIFF
            using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
            {
                tiffDevice.Process(pdfDoc, 1, 1, tiffStream);
            }
        }

        Console.WriteLine($"TIFF image created at '{outputPath}'.");
    }
}
