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

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define resolution for the TIFF images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create a TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputPath}");
    }
}