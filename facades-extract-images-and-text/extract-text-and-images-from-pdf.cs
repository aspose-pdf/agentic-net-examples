using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string textOutputPath = "extracted_text.txt";
        const string imagesOutputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(imagesOutputDir);

        // PdfExtractor implements IDisposable; wrap in using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Enable both text and image extraction
            extractor.ExtractText();   // extracts all text
            extractor.ExtractImage();  // extracts all images

            // Save extracted text to a file
            extractor.GetText(textOutputPath);

            // Extract images page by page
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Text and images have been extracted successfully.");
    }
}