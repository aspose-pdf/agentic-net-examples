using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "image-{0}.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.StartPage = 5;
            extractor.EndPage = 10;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputFile = string.Format(outputPattern, imageIndex);
                bool success = extractor.GetNextImage(outputFile, ImageFormat.Png);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image {imageIndex}");
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}