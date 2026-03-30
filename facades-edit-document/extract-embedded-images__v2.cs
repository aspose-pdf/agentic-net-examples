using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string outputFile = Path.Combine(outputFolder, "image-" + imageIndex + ".png");
            bool success = extractor.GetNextImage(outputFile, ImageFormat.Png);
            if (success)
            {
                Console.WriteLine("Extracted: " + outputFile);
            }
            else
            {
                Console.Error.WriteLine("Failed to extract image " + imageIndex);
            }
            imageIndex++;
        }

        extractor.Close();
    }
}