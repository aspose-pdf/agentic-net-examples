using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfImageExtractor
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string inputPdfPath = "input.pdf";

        // Output directory for extracted images
        const string outputDir = "ExtractedImages";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Create the extractor facade
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Set extraction mode (extract only images actually used on pages)
            // The ExtractImageMode enum resides in the Aspose.Pdf namespace, not in Facades.
            extractor.ExtractImageMode = Aspose.Pdf.ExtractImageMode.ActuallyUsed;

            // Optional: set higher resolution for clearer images
            extractor.Resolution = 300;

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to a file
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'.");
                imageIndex++;
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during extraction: {ex.Message}");
        }
    }
}
