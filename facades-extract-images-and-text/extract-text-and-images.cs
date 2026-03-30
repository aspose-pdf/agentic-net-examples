using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string textOutputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPath);

            // Enable text extraction
            extractor.ExtractText();

            // Enable image extraction
            extractor.ExtractImage();

            // Save the extracted text to a file
            extractor.GetText(textOutputPath);

            // Extract all images sequentially
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = $"image-{imageIndex}.png";
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}