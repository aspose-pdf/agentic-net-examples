using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure TIFF settings: multi‑page output, LZW compression, 300 DPI
            var tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,
                // Optional: set colour depth if required, e.g. Depth = ColorDepth.Format8bpp
            };

            var resolution = new Resolution(300);
            var tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert all pages to a multi‑page TIFF file
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
    }
}
