using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.tif";
        const string defaultFontName = "Arial";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define resolution for the TIFF output
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (no compression, default depth, landscape orientation)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Apply a default font for rendering any missing fonts in the source PDF
            tiffDevice.RenderingOptions.DefaultFontName = defaultFontName;

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}