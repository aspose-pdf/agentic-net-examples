using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Use the directory where the executable runs as the base folder.
        // In a real project replace this with the actual folder that contains the PDF.
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;

        // Name of the PDF file to convert. Ensure the file exists in the folder above.
        string pdfFile = "sample.pdf"; // <-- change to your file name

        // Full path to the source PDF.
        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Output TIFF file path (all pages merged into one multi‑page TIFF).
        string outputTiff = Path.Combine(dataDir, "AllPagesToTIFF_out.tif");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution for the output image (300 DPI is common).
            Resolution resolution = new Resolution(300);

            // Configure TIFF conversion settings.
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,   // No compression
                Depth = ColorDepth.Default,           // Default color depth
                Shape = ShapeType.Landscape,          // Landscape orientation
                SkipBlankPages = false                // Include blank pages
            };

            // Create the TIFF device with the resolution and settings.
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the entire PDF (all pages) to a single multi‑page TIFF file.
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"TIFF file created at: {outputTiff}");
    }
}
