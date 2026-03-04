using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "sample.pdf";

        // Output directories for extracted images and text
        const string imagesOutputDir = "ExtractedImages";
        const string textOutputDir   = "ExtractedText";

        // Define the page range (inclusive) from which to extract content
        const int startPage = 2; // first page in the range (1‑based)
        const int endPage   = 5; // last page in the range (1‑based)

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output folders exist
        Directory.CreateDirectory(imagesOutputDir);
        Directory.CreateDirectory(textOutputDir);

        // Use PdfExtractor (facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Set the page range for both image and text extraction
            extractor.StartPage = startPage;
            extractor.EndPage   = endPage;

            // -------------------------
            // Extract images from the range
            // -------------------------
            extractor.ExtractImage();

            int imageCounter = 1;
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each extracted image
                string imageFileName = Path.Combine(
                    imagesOutputDir,
                    $"page{extractor.StartPage}_img{imageCounter}.jpg");

                // Save the next image to the file system
                extractor.GetNextImage(imageFileName);

                imageCounter++;
            }

            // -------------------------
            // Extract text from the same range
            // -------------------------
            extractor.ExtractText();

            // Save all extracted text to a single .txt file
            string textFilePath = Path.Combine(
                textOutputDir,
                $"pages_{startPage}_to_{endPage}.txt");

            extractor.GetText(textFilePath);
        }

        Console.WriteLine("Extraction completed.");
    }
}