using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputPattern = "image-{0}.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputFile = string.Format(outputPattern, imageIndex);
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
