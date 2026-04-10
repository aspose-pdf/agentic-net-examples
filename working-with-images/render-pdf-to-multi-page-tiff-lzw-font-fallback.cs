using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // TiffDevice, Resolution, TiffSettings, CompressionType

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputTiffPath = "output.tif"; // destination TIFF
        const string fallbackFontName = "Arial";    // default font for missing characters

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set a default font to be used when a required font is missing
            PdfSaveOptions fontOptions = new PdfSaveOptions
            {
                DefaultFontName = fallbackFontName
            };

            // Save to a memory stream with the font option to apply the fallback
            using (MemoryStream tempStream = new MemoryStream())
            {
                pdfDoc.Save(tempStream, fontOptions);
                tempStream.Position = 0; // rewind for reading

                // Reload the document from the stream so the fallback font is applied
                using (Document docWithFallback = new Document(tempStream))
                {
                    // Define the resolution for the TIFF image (e.g., 300 DPI)
                    Resolution resolution = new Resolution(300);

                    // Configure TIFF settings with LZW compression
                    TiffSettings tiffSettings = new TiffSettings
                    {
                        Compression = CompressionType.LZW
                    };

                    // Create the TIFF device with the resolution and settings
                    TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                    // Render all pages of the PDF into a single multi‑page TIFF file
                    tiffDevice.Process(docWithFallback, outputTiffPath);
                }
            }
        }

        Console.WriteLine($"TIFF image saved to '{outputTiffPath}'.");
    }
}