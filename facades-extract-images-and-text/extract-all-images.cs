using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.StartPage = 1; // first page
            extractor.EndPage = 0;   // 0 means extract to the last page (all pages)
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // optional, extracts only shown images
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputFile = $"image-{imageIndex}.png"; // simple filename, no path
                extractor.GetNextImage(outputFile);
                Console.WriteLine($"Saved {outputFile}");
                imageIndex++;
            }

            extractor.Close();
        }
    }
}