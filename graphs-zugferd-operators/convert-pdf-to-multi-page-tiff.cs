using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // TiffDevice, Resolution, TiffSettings

class PdfToMultiPageTiff
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output multi‑page TIFF file path
        const string outputTiffPath = "output.tif";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution (DPI) for the resulting TIFF images
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (default settings are sufficient for most cases)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,   // No compression
                Depth = ColorDepth.Default,           // Default color depth
                Shape = ShapeType.Landscape,          // Landscape orientation
                SkipBlankPages = false                // Include blank pages
            };

            // Create the TiffDevice with the specified resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF to a multi‑page TIFF file.
            // Each PDF page becomes a separate image layer in the TIFF.
            tiffDevice.Process(pdfDocument, outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
    }
}