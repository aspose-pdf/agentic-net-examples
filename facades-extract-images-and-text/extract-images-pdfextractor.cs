using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        // Extract only the images that are actually displayed on the pages
        extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string outputFile = $"image-{imageIndex}.png"; // original format is preserved
            extractor.GetNextImage(outputFile);
            imageIndex++;
        }

        extractor.Close();
        Console.WriteLine($"Extracted {imageIndex - 1} images from '{inputPdf}'.");
    }
}