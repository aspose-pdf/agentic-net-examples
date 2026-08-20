using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file containing images
        const string inputPdf = @"C:\Docs\sample.pdf";

        // UNC network share destination (ensure proper UNC format)
        const string uncFolder = @"\\ServerName\SharedFolder\ExtractedImages";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the UNC directory exists (creates it if missing)
        try
        {
            Directory.CreateDirectory(uncFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create UNC directory '{uncFolder}': {ex.Message}");
            return;
        }

        // Initialize the PDF extractor (Facades API)
        PdfExtractor extractor = new PdfExtractor();

        // Bind the source PDF
        extractor.BindPdf(inputPdf);

        // Extract images from the PDF
        extractor.ExtractImage();

        int imageIndex = 1;
        // Loop through all extracted images
        while (extractor.HasNextImage())
        {
            // Build a file name for each image (JPEG format by default)
            string imagePath = Path.Combine(uncFolder, $"image-{imageIndex}.jpg");

            // Save the current image to the UNC path
            extractor.GetNextImage(imagePath);

            Console.WriteLine($"Saved image {imageIndex} to: {imagePath}");
            imageIndex++;
        }

        Console.WriteLine("Image extraction completed.");
    }
}