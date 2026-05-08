using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Local PDF file containing images to extract
        const string inputPdfPath = @"C:\Docs\sample.pdf";

        // Network share UNC path where images will be saved
        // Ensure the path starts with double backslashes and does not end with a backslash
        const string uncBasePath = @"\\ServerName\SharedFolder\ExtractedImages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the UNC directory exists; create it if necessary
        try
        {
            Directory.CreateDirectory(uncBasePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create UNC directory '{uncBasePath}': {ex.Message}");
            return;
        }

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF document
            extractor.BindPdf(inputPdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through each extracted image
            while (extractor.HasNextImage())
            {
                // Build a file name for each image (e.g., image-1.jpg)
                // PdfExtractor.GetNextImage defaults to JPEG format if no format is specified
                string fileName = $"image-{imageIndex}.jpg";

                // Combine UNC base path with file name
                string outputPath = Path.Combine(uncBasePath, fileName);

                // Save the current image to the UNC location
                extractor.GetNextImage(outputPath);

                Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}