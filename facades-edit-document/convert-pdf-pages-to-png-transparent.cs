using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PageImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Convert pages 2‑4 to PNG with 200 DPI and transparent background
        for (int pageNumber = 2; pageNumber <= 4 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            // Set resolution (DPI)
            Resolution resolution = new Resolution(200);

            // Create a PNG device with the required resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
            {
                // Render the specific page to the PNG stream
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("Selected pages have been converted to PNG images.");
    }
}
