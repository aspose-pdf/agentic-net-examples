using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file containing images
        const string pdfPath = "input.pdf";

        // Output folder for extracted images
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use a using block so PdfExtractor is disposed automatically
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each image while they are available
            while (extractor.HasNextImage())
            {
                // Build a file name for the extracted image
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");

                // Save the next image to the file (default format is JPEG)
                extractor.GetNextImage(imagePath);

                Console.WriteLine($"Extracted image {imageIndex} to '{imagePath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}