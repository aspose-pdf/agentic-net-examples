using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, output directory, DPI
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: PdfToTiffConverter <input.pdf> <outputDir> <dpi>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];
        if (!int.TryParse(args[2], out int dpi) || dpi <= 0)
        {
            Console.Error.WriteLine("Invalid DPI value.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define high‑resolution settings
            Resolution resolution = new Resolution(dpi);

            // Optional TIFF settings (no compression, default depth)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Initialize the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Process each page (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.tif");
                using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to a TIFF image
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
            }
        }
    }
}