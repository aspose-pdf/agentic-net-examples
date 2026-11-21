using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor (Facades) extracts images, text, etc. from a PDF.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document.
                extractor.BindPdf(inputPath);

                // StartPage = 1, EndPage = 0 indicates extraction from all pages.
                extractor.StartPage = 1;
                extractor.EndPage   = 0;

                // Perform image extraction for the defined page range.
                extractor.ExtractImage();

                // Save each extracted image to the output directory.
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}