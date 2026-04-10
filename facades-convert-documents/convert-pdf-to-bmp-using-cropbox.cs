using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades; // for PageCoordinateType enum

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output directory for BMP images
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution (DPI) for the output images
            Resolution resolution = new Resolution(300);

            // Initialize the BMP device with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Use CropBox coordinates so that margins are automatically cropped
            bmpDevice.CoordinateType = PageCoordinateType.CropBox;

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and save it
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as BMP → {outputPath}");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}