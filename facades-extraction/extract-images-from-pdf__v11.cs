using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Extract all images from the PDF
            extractor.ExtractImage();

            int imageCount = 0;

            // Iterate through each extracted image
            while (extractor.HasNextImage())
            {
                imageCount++;
                string imagePath = Path.Combine(outputDir, $"image_{imageCount}.jpg");

                // Save the current image as a JPG file
                extractor.GetNextImage(imagePath);
            }

            Console.WriteLine($"Total images extracted: {imageCount}");
        }
    }
}