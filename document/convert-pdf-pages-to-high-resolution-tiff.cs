using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main(string[] args)
    {
        // Expected arguments:
        //   args[0] - path to the source PDF file
        //   args[1] - output directory for TIFF images
        //   args[2] - (optional) DPI resolution (integer). Default is 300.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToTiffConverter <inputPdf> <outputFolder> [dpi]");
            return;
        }

        string inputPdfPath = args[0];
        string outputFolder = args[1];
        int dpi = 300; // default DPI

        if (args.Length >= 3 && !int.TryParse(args[2], out dpi))
        {
            Console.Error.WriteLine("Invalid DPI value. Using default 300 DPI.");
            dpi = 300;
        }

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object with the desired DPI
            Resolution resolution = new Resolution(dpi);

            // Configure TiffSettings (no compression, default depth, landscape orientation)
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
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.tif");

                // Convert the current page to TIFF and write it to a file stream
                using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
                {
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}