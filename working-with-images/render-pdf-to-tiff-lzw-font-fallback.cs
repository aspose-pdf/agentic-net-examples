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
        const string fallbackFont = "Arial";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure default font fallback for missing characters
            PdfSaveOptions pdfOpts = new PdfSaveOptions
            {
                DefaultFontName = fallbackFont
            };

            // Save to a memory stream with the font fallback applied
            using (MemoryStream ms = new MemoryStream())
            {
                pdfDoc.Save(ms, pdfOpts);
                ms.Position = 0; // Reset stream position for reading

                // Reload the PDF from the stream so the fallback takes effect
                using (Document docWithFallback = new Document(ms))
                {
                    // Set up TIFF settings with LZW compression
                    TiffSettings tiffSettings = new TiffSettings
                    {
                        Compression = CompressionType.LZW
                    };

                    // Define the resolution (e.g., 300 DPI)
                    Resolution resolution = new Resolution(300);

                    // Create the TIFF device with the specified resolution and settings
                    TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                    // Render all pages of the PDF into a single TIFF file
                    tiffDevice.Process(docWithFallback, outputPath);
                }
            }
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}