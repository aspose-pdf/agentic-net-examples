using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all images defined in the PDF resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Set a high resolution to keep the original image quality
            extractor.Resolution = 300; // DPI (adjust as needed)

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Save the image in PNG format (preserves resolution)
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}