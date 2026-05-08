using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Extract all images; this keeps original resolution and color depth
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through each extracted image
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.bmp");

                // Save the image as BMP preserving its native properties
                extractor.GetNextImage(outputPath, ImageFormat.Bmp);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and saved as BMP files.");
    }
}