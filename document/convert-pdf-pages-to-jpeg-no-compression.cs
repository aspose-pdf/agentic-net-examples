using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based (global rule)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Create a PNG device; default settings already produce lossless PNGs.
                // If a specific resolution is required, it can be passed to the constructor.
                PngDevice pngDevice = new PngDevice();

                string outPath = Path.Combine(outputDir, $"Page_{i}.png");

                // Convert the page to an image file
                using (FileStream imageStream = File.Create(outPath))
                {
                    pngDevice.Process(pdfDoc.Pages[i], imageStream);
                }

                Console.WriteLine($"Saved page {i} as PNG to '{outPath}'.");
            }
        }
    }
}
