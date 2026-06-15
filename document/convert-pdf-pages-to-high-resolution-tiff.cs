using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdfPath> <outputDirectory> <dpi>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: PdfToTiffConverter <input.pdf> <outputDir> <dpi>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDirectory = args[1];
        if (!int.TryParse(args[2], out int dpi) || dpi <= 0)
        {
            Console.Error.WriteLine("Invalid DPI value.");
            return;
        }

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object with the desired DPI
            Resolution resolution = new Resolution(dpi);

            // Configure TIFF settings (no compression, default depth, landscape orientation)
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Initialize the TiffDevice with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Iterate through all pages (Aspose.Pdf uses 1‑based page indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDirectory, $"page_{pageNumber}.tif");

                // Process each page and write the TIFF image to a file stream
                using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
                {
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
            }
        }
    }
}