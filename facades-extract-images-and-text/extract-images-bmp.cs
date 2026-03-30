using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);
        converter.DoConvert();

        int imageIndex = 1;
        while (converter.HasNextImage())
        {
            string outputFile = "image" + imageIndex + ".bmp";
            converter.GetNextImage(outputFile, ImageFormat.Bmp);
            Console.WriteLine($"Saved {outputFile}");
            imageIndex++;
        }

        Console.WriteLine("Image extraction completed.");
    }
}