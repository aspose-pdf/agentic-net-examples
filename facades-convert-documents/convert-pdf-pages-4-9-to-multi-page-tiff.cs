using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output TIFF file path
        const string outputTiffPath = "output_pages_4_to_9.tif";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputTiffPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a resolution object (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings (optional – adjust as needed)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Initialize the TiffDevice with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert pages 4 through 9 of the PDF to a single multi‑page TIFF file
            // The overload Process(Document, int fromPage, int toPage, string outputPath) is used
            tiffDevice.Process(pdfDocument, 4, 9, outputTiffPath);
        }

        Console.WriteLine($"TIFF conversion completed. Output saved to '{outputTiffPath}'.");
    }
}