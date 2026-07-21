using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfExtractionDemo
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string textOutputPath = "extracted_text.txt";
        const string imagesOutputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesOutputFolder);

        // Create and configure the extractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // Enable text extraction (pure text mode = 0)
            extractor.ExtractTextMode = 0;

            // Image extraction mode property does not exist in recent Aspose.Pdf versions.
            // The default behavior extracts all images, so no explicit setting is required.

            // Perform extraction
            extractor.ExtractText();
            extractor.ExtractImage();

            // Save extracted text to a file
            extractor.GetText(textOutputPath);
            Console.WriteLine($"Text extracted to: {textOutputPath}");

            // Save each extracted image to a separate file
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesOutputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                imageIndex++;
            }
        }
    }
}
