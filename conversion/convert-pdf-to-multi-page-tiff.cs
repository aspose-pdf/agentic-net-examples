using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output TIFF file path (all pages merged into a single TIFF)
        const string outputTiffPath = "output.tif";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution (DPI) for the resulting TIFF image
            Resolution resolution = new Resolution(300);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,   // No compression
                Depth = ColorDepth.Default,           // Default color depth
                Shape = ShapeType.Landscape,          // Landscape orientation
                SkipBlankPages = false                // Include blank pages
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF document to a single multi-page TIFF file
            tiffDevice.Process(pdfDocument, outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
    }
}