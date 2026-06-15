using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // TiffDevice, Resolution, TiffSettings, CompressionType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tif";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define the resolution for the TIFF output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings with LZW compression
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW
                // Other settings can remain default
            };

            // Create the TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Set a default font to use when characters are missing in the source PDF
            tiffDevice.RenderingOptions.DefaultFontName = "Arial";

            // Render all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDoc, outputPath);
        }

        Console.WriteLine($"TIFF file saved to '{outputPath}'.");
    }
}