using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdfPath = "input.pdf";

        // Output TIFF file (single‑page image)
        const string outputTiffPath = "first_page.tif";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution for the TIFF image (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                // Use LZW compression (a widely supported compression type)
                Compression = CompressionType.LZW,
                Depth = ColorDepth.Format1bpp,               // 1‑bit per pixel
                Shape = ShapeType.Portrait,                  // portrait orientation
                SkipBlankPages = false                       // do not skip blank pages
            };

            // Create the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page (pages are 1‑based) to a single‑page TIFF file
            tiffDevice.Process(pdfDocument.Pages[1], outputTiffPath);
        }

        Console.WriteLine($"First page saved as TIFF: {outputTiffPath}");
    }
}
