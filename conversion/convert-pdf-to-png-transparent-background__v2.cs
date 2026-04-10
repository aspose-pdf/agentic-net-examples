using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PngOutput";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a PNG device with a desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for pages that contain an alpha channel
                TransparentBackground = true
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to PNG using the configured device
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved PNG: {outPath}");
            }
        }
    }
}