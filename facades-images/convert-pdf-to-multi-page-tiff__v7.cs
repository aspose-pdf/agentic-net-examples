using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution, TiffDevice, TiffSettings, CompressionType

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDoc = new Document(inputPdfPath);

            // Set the desired resolution (DPI).
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings – here we use LZW compression, you can adjust as needed.
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW,
                Depth = ColorDepth.Format8bpp,
                Shape = ShapeType.Portrait,
                SkipBlankPages = false
            };

            // Create a TiffDevice which can render the whole document into a multi‑page TIFF.
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Process all pages (from 1 to pdfDoc.Pages.Count) and write the result to the output file.
            tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, outputTiffPath);

            Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
