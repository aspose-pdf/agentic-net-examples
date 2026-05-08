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
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define the resolution for the TIFF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Apply a default font for rendering any missing fonts in the PDF
            tiffDevice.RenderingOptions.DefaultFontName = "Arial";

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputPath);
        }

        Console.WriteLine($"TIFF file saved to '{outputPath}'.");
    }
}