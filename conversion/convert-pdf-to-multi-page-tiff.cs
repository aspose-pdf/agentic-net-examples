using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Configure TIFF settings (compression, DPI, etc.)
        Resolution resolution = new Resolution(300); // desired DPI
        TiffSettings tiffSettings = new TiffSettings
        {
            Compression = CompressionType.CCITT4 // LZW, CCITT4, CCITT3, RLE, None are valid values
        };

        // Create the TiffDevice with the resolution and settings
        TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

        // Write the multi‑page TIFF to a file stream
        using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Convert all pages (1 .. pdfDoc.Pages.Count) into a single TIFF file
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, tiffStream);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputPath}");
    }
}
