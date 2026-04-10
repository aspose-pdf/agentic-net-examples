using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Resolution, TiffDevice, TiffSettings, CompressionType, ColorDepth, ShapeType

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tif";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set desired resolution for the TIFF images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,   // No compression
                Depth = ColorDepth.Default,           // Default color depth
                Shape = ShapeType.Landscape,          // Landscape orientation
                SkipBlankPages = false                // Include blank pages
            };

            // Create the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiff}");
    }
}