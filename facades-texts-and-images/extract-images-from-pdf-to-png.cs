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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract images from the PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build output file name (PNG format)
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the next image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Images saved to '{outputFolder}'.");
    }
}