using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.StartPage = 2;
        extractor.EndPage = 2;
        extractor.ExtractImage();

        if (extractor.HasNextImage())
        {
            bool success = extractor.GetNextImage(outputPath, ImageFormat.Png);
            if (success)
            {
                Console.WriteLine($"First image from page 2 saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to extract image.");
            }
        }
        else
        {
            Console.WriteLine("No images found on page 2.");
        }
    }
}