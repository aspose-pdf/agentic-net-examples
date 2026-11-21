using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor is a disposable Facade – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Configure the extractor to pull images (original format)
            // In current API the mode is set by calling ExtractImage()
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop while there are images to retrieve
            while (extractor.HasNextImage())
            {
                // Save each image using the overload that preserves the original format
                string imagePath = Path.Combine(outputDir, $"Image_{imageIndex}.img");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Extracted image {imageIndex} to {imagePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}