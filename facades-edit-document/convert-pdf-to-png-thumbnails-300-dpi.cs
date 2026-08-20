using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a Resolution object with 300 DPI (custom DPI)
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Enable transparent background for the generated PNGs
            pngDevice.TransparentBackground = true;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each thumbnail
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Create a file stream for the PNG output
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to PNG and write it to the stream
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PNG thumbnails generated successfully.");
    }
}
