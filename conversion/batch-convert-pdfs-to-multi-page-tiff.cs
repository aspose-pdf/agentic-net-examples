using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToTiff
{
    static void Main()
    {
        // Input PDF files – adjust the array as needed
        string[] pdfFiles = {
            @"C:\Input\Document1.pdf",
            @"C:\Input\Document2.pdf",
            @"C:\Input\Document3.pdf"
        };

        // Directory where the resulting TIFF files will be saved
        string outputDir = @"C:\Output\TiffArchives";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Build the output TIFF file name (same base name as the PDF)
            string tiffPath = Path.Combine(
                outputDir,
                Path.GetFileNameWithoutExtension(pdfPath) + ".tiff");

            // Load the PDF document inside a using block for proper disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Default resolution (150 DPI) – can be changed if required
                Resolution resolution = new Resolution(150);

                // Default TIFF settings with no compression
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.None,   // default compression
                    Depth = ColorDepth.Default,
                    Shape = ShapeType.Landscape,
                    SkipBlankPages = false
                };

                // Create the TIFF device with the resolution and settings
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // Convert the entire PDF to a multi‑page TIFF file
                tiffDevice.Process(pdfDocument, tiffPath);
            }

            Console.WriteLine($"Converted '{pdfPath}' → '{tiffPath}'");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}