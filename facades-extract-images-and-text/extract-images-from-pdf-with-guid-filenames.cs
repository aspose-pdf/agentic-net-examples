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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                string guid = Guid.NewGuid().ToString();
                string outputPath = Path.Combine(outputFolder, $"{guid}.png");
                extractor.GetNextImage(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}