using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPrefix = "image_";
        const string outputSuffix = ".png";
        const int maxSize = 200;

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
            string outputFile = outputPrefix + imageIndex + outputSuffix;
            // Save thumbnail with maximum width and height of 200 pixels
            converter.GetNextImage(outputFile, ImageFormat.Png, maxSize, maxSize);
            imageIndex++;
        }

        Console.WriteLine($"Extracted {imageIndex - 1} images as PNG thumbnails.");
    }
}