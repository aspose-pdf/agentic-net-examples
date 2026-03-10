using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor facade to pull out images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Preserve original image quality – use the default mode (DefinedInResources)
            // or explicitly set it if desired
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for each extracted image
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Extract the image; GetNextImage returns true on success
                bool success = extractor.GetNextImage(imagePath);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                    break;
                }

                Console.WriteLine($"Extracted image #{imageIndex} to '{imagePath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}