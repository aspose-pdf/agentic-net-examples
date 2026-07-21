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

        // Output multi‑page TIFF file path
        const string outputTiffPath = "output.tif";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution (DPI) for the resulting TIFF images
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (no compression, default color depth, landscape orientation)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
    }
}