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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Initialize the extractor and bind the PDF file
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdf);

            // Extract all images from the PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image with a unique name; extension can be .png, .jpg, etc.
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images were found in the PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting images: {ex.Message}");
        }
    }
}