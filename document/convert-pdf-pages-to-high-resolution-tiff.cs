using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdfPath> <outputFolder> [dpi]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToTiffConverter <input.pdf> <outputFolder> [dpi]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        int dpi = args.Length >= 3 ? int.Parse(args[2]) : 300; // Default to 300 DPI if not specified

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPath))
            {
                // Create a Resolution object with the desired DPI
                Resolution resolution = new Resolution(dpi);

                // Configure TiffSettings (optional: no compression for lossless output)
                TiffSettings tiffSettings = new TiffSettings
                {
                    Compression = CompressionType.None,
                    Depth = ColorDepth.Default,
                    Shape = ShapeType.Landscape,
                    SkipBlankPages = false
                };

                // Initialize the TiffDevice with the resolution and settings
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Construct the output file name for each page
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.tif");

                    // Convert the specific page to a TIFF image and save it
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
                }
            }

            Console.WriteLine("PDF pages successfully converted to high‑resolution TIFF images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}