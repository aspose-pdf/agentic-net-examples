using System;
using System.IO;
using System.Drawing.Imaging;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (implements IDisposable) to bind and extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(inputPdf);

            // Optional: extract only images actually used on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Begin the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                // Save each image as PNG (change ImageFormat if another format is desired)
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}