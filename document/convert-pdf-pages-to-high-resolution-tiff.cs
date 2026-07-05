using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for TIFF images
        const string outputDir = "TiffPages";

        // Desired DPI (resolution) for the output images
        const int dpi = 300;

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Set the resolution for the TIFF images
                Resolution resolution = new Resolution(dpi);

                // Configure TIFF settings (no compression, default color depth, landscape orientation)
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.None,
                    Depth = ColorDepth.Default,
                    Shape = ShapeType.Landscape,
                    SkipBlankPages = false
                };

                // Create a TiffDevice with the specified resolution and settings
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build the output file name for the current page
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.tif");

                    // Convert the current page to a TIFF image and write it to a file stream
                    using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
                    {
                        tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffStream);
                    }

                    Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}