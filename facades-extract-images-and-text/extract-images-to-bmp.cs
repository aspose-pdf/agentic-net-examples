using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf.Facades; // PdfExtractor facade

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";                 // source PDF
        const string outputFolder = "ExtractedImages";       // folder for BMP files

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor extracts images directly from the PDF resources,
        // preserving original resolution and color depth.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Extract all images defined in the PDF resources
            extractor.ExtractImage();

            int imageNumber = 1;
            // Iterate through each extracted image
            while (extractor.HasNextImage())
            {
                // Build output BMP file path
                string bmpPath = Path.Combine(outputFolder, $"image_{imageNumber}.bmp");

                // Save the image as BMP; the original image data is written unchanged
                extractor.GetNextImage(bmpPath, ImageFormat.Bmp);

                imageNumber++;
            }
        }

        Console.WriteLine("Image extraction to BMP completed successfully.");
    }
}
