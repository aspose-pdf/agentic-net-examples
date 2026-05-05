using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    // Entry point
    static void Main(string[] args)
    {
        // Expected arguments: inputPdfPath outputDirectory dpi
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: PdfToTiffConverter <input-pdf> <output-dir> <dpi>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDir    = args[1];
        if (!int.TryParse(args[2], out int dpi) || dpi <= 0)
        {
            Console.WriteLine("Invalid DPI value.");
            return;
        }

        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create resolution object with the desired DPI
            Resolution resolution = new Resolution(dpi);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Create a TIFF device using the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Process each page individually
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.tif");

                using (FileStream tiffStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the specific page to TIFF and write to the stream
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}