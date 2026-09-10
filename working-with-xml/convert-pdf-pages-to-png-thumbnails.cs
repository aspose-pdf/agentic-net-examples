using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Directory where PNG thumbnails will be saved
        const string outputDir = "Thumbnails";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Define a high resolution (e.g., 300 DPI) for the PNG images
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Build the output file name for the current page
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Convert the page to PNG and write it to a file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved thumbnail: {outPath}");
            }
        }
    }
}