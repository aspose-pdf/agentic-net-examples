using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string textOutputPath = "sample.txt";
        const string imageOutputPrefix = "image-";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create and configure the PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Enable text extraction (default mode is pure text)
            extractor.ExtractTextMode = 0; // optional, 0 = pure text mode

            // The ExtractImageMode property does not exist in the current Aspose.Pdf version.
            // Image extraction is performed simply by calling ExtractImage().
            extractor.ExtractText();
            extractor.ExtractImage();

            // Save extracted text to a file
            extractor.GetText(textOutputPath);

            // Save each extracted image to separate files
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = $"{imageOutputPrefix}{imageIndex}.png";
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("PDF extraction (text and images) completed.");
    }
}
