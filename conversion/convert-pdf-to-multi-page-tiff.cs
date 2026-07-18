using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Define the resolution for the output TIFF (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create a TiffDevice with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"TIFF file created: {outputTiff}");
    }
}