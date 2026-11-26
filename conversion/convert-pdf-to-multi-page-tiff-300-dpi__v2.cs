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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal of the Document object
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Optional: configure TIFF output options
            var tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,      // LZW, CCITT4, CCITT3, RLE, None
                Depth = ColorDepth.Format8bpp,          // Default, Format24bpp, Format8bpp, Format4bpp, Format1bpp
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create a TiffDevice with a 300 DPI resolution and the above settings
            TiffDevice tiffDevice = new TiffDevice(new Resolution(300), tiffSettings);

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, outputTiff);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiff}");
    }
}
