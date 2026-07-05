using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Use PdfExtractor (facade) to extract images from pages 2‑5
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);
                extractor.StartPage = 2;   // inclusive start page (1‑based)
                extractor.EndPage   = 5;   // inclusive end page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(tempDir, $"image_{imageIndex}.png");
                    // Save each image; default format is PNG if extension is .png
                    extractor.GetNextImage(outputPath);
                    Console.WriteLine($"Extracted image {imageIndex} to {outputPath}");
                    imageIndex++;
                }
            }

            Console.WriteLine($"All images saved to temporary directory: {tempDir}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}