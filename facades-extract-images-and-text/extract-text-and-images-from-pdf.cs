using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF file
        const string textOutput = "extracted.txt";   // text extraction result
        const string imagePrefix = "image-";          // prefix for extracted images

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create the extractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF file to the extractor
        extractor.BindPdf(pdfPath);

        // Enable text extraction
        extractor.ExtractText();

        // Enable image extraction
        extractor.ExtractImage();

        // Save all extracted text to a single file
        extractor.GetText(textOutput);
        Console.WriteLine($"Text extracted to '{textOutput}'.");

        // Save each extracted image to a separate file
        int imgIndex = 1;
        while (extractor.HasNextImage())
        {
            string imgPath = $"{imagePrefix}{imgIndex}.png";
            extractor.GetNextImage(imgPath);
            Console.WriteLine($"Image {imgIndex} saved to '{imgPath}'.");
            imgIndex++;
        }

        // Release resources
        extractor.Close();
    }
}