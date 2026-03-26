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

        using (Document pdfDocument = new Document(inputPath))
        {
            // Custom resolution (300 DPI)
            Resolution resolution = new Resolution(300);

            // Custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create the TIFF device with the custom settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page to a single‑page TIFF file
            tiffDevice.Process(pdfDocument, 1, 1, outputPath);
        }

        Console.WriteLine($"TIFF saved to '{outputPath}'.");
    }
}