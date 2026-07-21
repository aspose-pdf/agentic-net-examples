using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the generated PNGs
                TransparentBackground = true
            };

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Create a file stream for the output PNG
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to PNG and write to the stream
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail PNG images have been generated successfully.");
    }
}