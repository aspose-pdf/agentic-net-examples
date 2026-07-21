using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page2_image1.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract images from page 2 using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.StartPage = 2; // page indexing is 1‑based
            extractor.EndPage   = 2;
            extractor.ExtractImage();

            // Save the first found image as PNG
            if (extractor.HasNextImage())
            {
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                Console.WriteLine($"First image from page 2 saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}