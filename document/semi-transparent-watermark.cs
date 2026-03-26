using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string watermarkPath = "watermark.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            ImageStamp stamp = new ImageStamp(watermarkPath);
            stamp.Opacity = 0.5f;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(stamp);
            }

            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
