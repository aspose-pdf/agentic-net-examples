using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (which implements IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract images from the PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build the output file name (PNG format)
                string outputFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image as PNG
                extractor.GetNextImage(outputFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}