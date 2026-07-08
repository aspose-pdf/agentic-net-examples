using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "first_page.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify that the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Define the resolution for the output TIFF (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure custom TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.CCITT4,   // corrected enum value
                Depth = ColorDepth.Format1bpp,          // 1‑bit color depth
                Shape = ShapeType.Portrait,             // portrait orientation
                SkipBlankPages = false                  // do not skip blank pages
            };

            // Create the TiffDevice with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert only the first page (page numbers are 1‑based) to a single‑page TIFF
            using (FileStream tiffStream = new FileStream(outputTiff, FileMode.Create))
            {
                tiffDevice.Process(pdfDoc, 1, 1, tiffStream);
            }
        }

        Console.WriteLine($"Single‑page TIFF created at: {outputTiff}");
    }
}
