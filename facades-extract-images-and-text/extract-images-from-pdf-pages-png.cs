using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' not found. Extraction aborted.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to extract images from pages 5‑10
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Optional: extract only images that are actually rendered on the page
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            int imageCounter = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string outputPath = Path.Combine(outputFolder,
                    $"image-{imageCounter}.png");

                // Save the current image as PNG using System.Drawing.Imaging.ImageFormat
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageCounter++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
