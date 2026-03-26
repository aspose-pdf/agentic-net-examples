using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Use the directory where the executable resides as the base folder.
        // You can change this to any folder that contains your PDF file.
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;
        // Name of the PDF file to convert. Replace with your actual file name.
        string pdfFile = "sample.pdf";

        string inputPath = Path.Combine(dataDir, pdfFile);
        string outputPath = Path.Combine(dataDir, "AllPagesToTIFF_out.tif");

        // Verify that the input PDF exists before attempting conversion.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{inputPath}'. Please ensure the file exists and the path is correct.");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define resolution (DPI)
            Resolution resolution = new Resolution(300);

            // Configure TIFF settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Convert the whole PDF into a single multi‑page TIFF file
            tiffDevice.Process(pdfDocument, outputPath);
        }

        Console.WriteLine("PDF successfully converted to multi‑page TIFF: " + outputPath);
    }
}
