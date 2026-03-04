using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Folder where extracted images will be saved
        const string imagesFolder = "ExtractedImages";

        // File where extracted text will be saved
        const string textOutputFile = "extracted_text.txt";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the images output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Use PdfExtractor (implements IDisposable) to bind the PDF and perform extraction
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // -------------------------
            // Extract all images
            // -------------------------
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a separate file (default format is PNG)
                string imagePath = Path.Combine(imagesFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }

            // -------------------------
            // Extract all text
            // -------------------------
            extractor.ExtractText();

            // Save the extracted text to a single .txt file
            extractor.GetText(textOutputFile);
        }

        Console.WriteLine("Image and text extraction completed successfully.");
    }
}