using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a PNG device with desired resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for pages that contain alpha channel
                TransparentBackground = true
            };

            // Iterate over pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to PNG with transparent background
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("PDF to PNG conversion with transparent background completed.");
    }
}