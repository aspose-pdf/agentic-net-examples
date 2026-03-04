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

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                // Save the current image to a file (default format is JPEG; extension determines the saved file name)
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}