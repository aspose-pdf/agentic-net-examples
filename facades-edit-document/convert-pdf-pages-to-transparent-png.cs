using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Devices;      // Resolution, PngDevice, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where PNG images will be saved
        const string outputDirectory = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object for 150 DPI (default is 150, but set explicitly)
            Resolution resolution = new Resolution(150);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the generated PNGs
                TransparentBackground = true
            };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDirectory, $"page_{pageNumber}.png");

                // Convert the page to PNG and write it to a file stream
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("All pages have been converted to PNG images with transparent background at 150 DPI.");
    }
}
