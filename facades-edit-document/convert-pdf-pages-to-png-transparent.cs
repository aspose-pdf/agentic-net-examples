using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (1‑based page indexing)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a resolution of 200 DPI
            Resolution resolution = new Resolution(200);

            // Initialize PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the PNG output
                TransparentBackground = true
            };

            // Convert pages 2‑4 (or up to the last page if fewer)
            for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save each page as a PNG image
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
            }
        }
    }
}