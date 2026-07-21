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
        // Output directory for BMP images (ensure it exists)
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using the Document constructor)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object with 150 DPI (default, but set explicitly)
            Resolution resolution = new Resolution(150);

            // Initialize BmpDevice with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Determine how many pages to process (pages 1‑20 or up to the document's page count)
            int maxPage = Math.Min(20, pdfDocument.Pages.Count);

            for (int pageNumber = 1; pageNumber <= maxPage; pageNumber++)
            {
                // Build output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Convert the specific page to BMP and save via a FileStream
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
