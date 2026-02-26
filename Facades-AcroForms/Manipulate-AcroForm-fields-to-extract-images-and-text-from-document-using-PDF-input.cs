using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputText = "extracted.txt";
        const string imageFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(imageFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set extraction modes (int for text, enum for images)
            extractor.ExtractTextMode = 0; // 0 = pure text mode
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Perform extraction
            extractor.ExtractText();
            extractor.ExtractImage();

            // Save extracted text to a file
            extractor.GetText(outputText);

            // Save each extracted image to the image folder
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imageFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction of text and images completed.");
    }
}