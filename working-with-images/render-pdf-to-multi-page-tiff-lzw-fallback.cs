using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tif";
        const string fallbackFontName = "Arial";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Register a fallback font for any missing glyphs
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", fallbackFontName));

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution for the TIFF image (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings with LZW compression
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.LZW
            };

            // Create a TIFF device with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Render all pages of the PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputTiffPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputTiffPath}'.");
    }
}