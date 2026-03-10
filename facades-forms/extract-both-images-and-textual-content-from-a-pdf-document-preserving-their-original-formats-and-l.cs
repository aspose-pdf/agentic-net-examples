using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output locations
        const string outputTextPath = "extracted_text.txt";
        const string imagesOutputFolder = "ExtractedImages";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesOutputFolder);

        // -------------------------------------------------
        // Use PdfExtractor (Facade) to extract text & images
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // -----------------
            // Extract all text
            // -----------------
            extractor.ExtractText();                     // Perform text extraction
            extractor.GetText(outputTextPath);           // Save extracted text to a file

            // -----------------
            // Extract all images
            // -----------------
            extractor.ExtractImage();                    // Prepare image extraction
            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imageFile = Path.Combine(
                    imagesOutputFolder,
                    $"image_{imageIndex}.jpg"); // Default format is JPEG

                // Save the current image to disk
                extractor.GetNextImage(imageFile);

                imageIndex++;
            }
        }

        Console.WriteLine("Extraction of text and images completed successfully.");
    }
}