using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // TiffDevice, Resolution, TiffSettings, CompressionType, ColorDepth, ShapeType

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string outputTiff = "output.tif";  // destination multi‑page TIFF
        const string defaultFont = "Arial";      // font used when a glyph is missing

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a resolution object (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,   // no compression
                Depth = ColorDepth.Default,           // default color depth
                Shape = ShapeType.Landscape,          // landscape orientation
                SkipBlankPages = false                // include blank pages
            };

            // Instantiate the TIFF device with resolution and settings (lifecycle: create)
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Apply a default font for rendering missing fonts
            tiffDevice.RenderingOptions.DefaultFontName = defaultFont;

            // Convert the entire PDF to a multi‑page TIFF file (lifecycle: save)
            tiffDevice.Process(pdfDoc, outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}