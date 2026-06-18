using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document with deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Define the resolution for the output PNG images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a PNG device, set the resolution and enable transparent background
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to PNG and write to the file stream
                    pngDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images with transparent background.");
    }
}