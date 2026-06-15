using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output multi‑page TIFF file path
        const string outputTiff = "output.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Set up resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (no compression, default color depth, landscape orientation)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Apply a default font for rendering missing fonts
            tiffDevice.RenderingOptions = new RenderingOptions
            {
                // Use a font that is available on the system (e.g., Arial)
                DefaultFontName = "Arial"
            };

            // Convert the entire PDF (pages 1 to last) to a multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}