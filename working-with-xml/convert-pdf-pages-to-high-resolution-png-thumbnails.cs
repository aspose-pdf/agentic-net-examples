using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice, Resolution

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where PNG thumbnails will be saved
        const string outputDir = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define a high‑resolution (e.g., 300 DPI) for the output images
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Convert the page to PNG and write it to a file stream
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG: {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to high‑resolution PNG thumbnails.");
    }
}