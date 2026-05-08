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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define a high resolution (e.g., 300 DPI) for thumbnail quality
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Create a file stream for each PNG output
                using (FileStream pngStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to PNG and write it to the stream
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been created successfully.");
    }
}