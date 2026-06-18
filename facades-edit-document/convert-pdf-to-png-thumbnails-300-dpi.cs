using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output directory for PNG thumbnails
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize PngDevice with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the generated PNGs
                TransparentBackground = true
            };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output PNG file name
                string outputPngPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Write the PNG image to a file stream
                using (FileStream pngStream = new FileStream(outputPngPath, FileMode.Create))
                {
                    // Convert the specific page to PNG and save it
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG: {outputPngPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}