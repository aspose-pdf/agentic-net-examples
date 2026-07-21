using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades; // Included as per task requirement

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a Resolution object for 200 DPI
            Resolution resolution = new Resolution(200);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Enable transparent background for the PNG output
            pngDevice.TransparentBackground = true;

            // Convert pages 2 through 4 (inclusive). Guard against documents with fewer pages.
            for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Write each page to a separate PNG file
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG: {outputPath}");
            }
        }
    }
}