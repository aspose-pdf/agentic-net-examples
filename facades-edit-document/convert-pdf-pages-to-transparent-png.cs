using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice, Resolution

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "PngPages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a Resolution object for 150 DPI (default, but set explicitly)
            Resolution resolution = new Resolution(150);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the generated PNGs
                TransparentBackground = true
            };

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Convert the current page to PNG and write to file
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG → {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to PNG images.");
    }
}