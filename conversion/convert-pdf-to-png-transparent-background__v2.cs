using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a PNG device with a desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for pages that contain alpha channel
                TransparentBackground = true
            };

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save each page as a PNG file
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PDF to PNG conversion with transparent background completed.");
    }
}