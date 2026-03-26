using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const int dpi = 300; // high‑resolution DPI

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a resolution object for the desired DPI
            Resolution resolution = new Resolution(dpi);

            // Configure TIFF conversion settings
            TiffSettings tiffSettings = new TiffSettings
            {
                Compression = CompressionType.None,
                Depth = ColorDepth.Default,
                Shape = ShapeType.Landscape,
                SkipBlankPages = false
            };

            // Initialize the TIFF device with the resolution and settings
            TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

            // Iterate through each page and save it as an individual TIFF file
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];
                string outputFile = $"page{pageNumber}.tif";

                using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    tiffDevice.Process(page, outputStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as {outputFile}");
            }
        }
    }
}