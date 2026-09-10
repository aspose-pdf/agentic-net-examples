using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the resolution for the TIFF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings: no compression, default color depth, landscape orientation
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Apply a default font for rendering when the original PDF references missing fonts
            tiffDevice.RenderingOptions.DefaultFontName = "Arial";

            // Convert the entire PDF to a multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}