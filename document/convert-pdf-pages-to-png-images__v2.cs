using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Input PDF path (adjust as needed)
        const string inputPdfPath = "sample.pdf";

        // Output directory for generated images
        const string outputDir = "PageImages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists and is empty
        if (Directory.Exists(outputDir))
        {
            foreach (var file in Directory.GetFiles(outputDir))
                File.Delete(file);
        }
        else
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int pageCount = pdfDoc.Pages.Count; // 1‑based page count

            // Convert each page to a PNG image
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string imagePath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Create a PNG device (default settings)
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    PngDevice pngDevice = new PngDevice();
                    // Process the specific page and write to the stream
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }

            // Verification: count generated image files
            int generatedImages = Directory.GetFiles(outputDir, "*.png").Length;

            if (generatedImages == pageCount)
            {
                Console.WriteLine($"Success: {generatedImages} image(s) generated for {pageCount} page(s).");
            }
            else
            {
                Console.Error.WriteLine($"Failure: Expected {pageCount} images, but found {generatedImages}.");
            }
        }
    }
}