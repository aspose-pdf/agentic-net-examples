using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: extract only images actually used on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                bool success = extractor.GetNextImage(outputFile);
                if (success)
                {
                    Console.WriteLine($"Extracted image {imageIndex} to {outputFile}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to extract image {imageIndex}");
                }
                imageIndex++;
            }
        }
    }
}