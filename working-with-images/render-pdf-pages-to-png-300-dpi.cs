using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output directory for PNG images
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
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

        Console.WriteLine("All pages have been rendered to PNG at 300 DPI.");
    }
}