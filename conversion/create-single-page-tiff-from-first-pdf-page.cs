using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "first_page.tif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify that the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Define custom resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Define custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,
                Depth = ColorDepth.Format8bpp,
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create a TiffDevice with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page (pages are 1‑based) to a single‑page TIFF file
            tiffDevice.Process(pdfDoc, 1, 1, outputPath);
        }

        Console.WriteLine($"Single‑page TIFF created at: {outputPath}");
    }
}