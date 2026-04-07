using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputImage = "image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            // Specify the page range (1‑based). We need only page 2.
            extractor.StartPage = 2;
            extractor.EndPage   = 2;
            extractor.ExtractImage();

            if (extractor.HasNextImage())
            {
                // Save the first image from page 2 as PNG
                bool saved = extractor.GetNextImage(outputImage, ImageFormat.Png);
                if (saved)
                {
                    Console.WriteLine("Image extracted to " + outputImage);
                }
                else
                {
                    Console.Error.WriteLine("Failed to extract image.");
                }
            }
            else
            {
                Console.Error.WriteLine("No images found on page 2.");
            }
        }
    }
}
