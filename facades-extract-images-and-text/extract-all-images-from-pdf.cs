using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor facade to extract images from all pages
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 1; // first page
            extractor.EndPage = 0;   // 0 indicates extraction till the last page
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Save each extracted image as PNG
                extractor.GetNextImage(outPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}