using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a unique temporary folder for the extracted images
        string tempDir = Path.Combine(Path.GetTempPath(),
                                      "ExtractedImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        Console.WriteLine($"Images will be saved to: {tempDir}");

        // Extract images from pages 2 through 5 using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);   // Load the PDF
            extractor.StartPage = 2;       // First page to process (1‑based)
            extractor.EndPage   = 5;       // Last page to process
            extractor.ExtractImage();      // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a JPEG file in the temporary folder
                string outputPath = Path.Combine(tempDir, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(outputPath);
                Console.WriteLine($"Saved image {imageIndex} → {outputPath}");
                imageIndex++;
            }
        }
    }
}