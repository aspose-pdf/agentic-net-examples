using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify that the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Configure TIFF settings (compression, color depth, etc.)
            var tiffSettings = new TiffSettings
            {
                Compression = CompressionType.CCITT4,   // CCITT4 compression
                Depth = ColorDepth.Format1bpp,          // 1‑bit per pixel (adjust if needed)
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create the TiffDevice – it does NOT implement IDisposable, so no using block
            var tiffDevice = new TiffDevice(new Resolution(300), tiffSettings);

            // Convert only the first page to a single‑page TIFF file
            tiffDevice.Process(pdfDoc, 1, 1, outputTiff);
        }

        Console.WriteLine($"First page saved as TIFF: {outputTiff}");
    }
}
