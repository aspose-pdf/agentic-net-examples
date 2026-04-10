using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // Optional: extract only images actually used on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int index = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{index}.jpg");
                extractor.GetNextImage(outputPath);
                index++;
            }
        }

        Console.WriteLine($"All images extracted to folder: {outputFolder}");
    }
}