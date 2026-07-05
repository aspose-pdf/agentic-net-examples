using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "sample.pdf";

        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so a using block ensures it is disposed automatically
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document to the extractor
                extractor.BindPdf(inputPdf);

                // Start the image extraction process
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each extracted image while there are more images available
                while (extractor.HasNextImage())
                {
                    // Save each image to a separate file (default format is JPEG)
                    string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.jpg");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'");
                    imageIndex++;
                }

                // No explicit call to Close() is required; the using block will call Dispose(),
                // which internally releases the bound Document.
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}