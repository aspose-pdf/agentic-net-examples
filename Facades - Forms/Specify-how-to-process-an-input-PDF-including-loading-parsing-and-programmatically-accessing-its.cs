using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class PdfProcessingExample
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output paths for extracted content
        const string extractedTextPath = "extracted_text.txt";
        const string imagesOutputDir = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // -------------------------------------------------
        // 1. Access PDF meta-information using PdfFileInfo
        // -------------------------------------------------
        int totalPages;
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdfPath))
        {
            Console.WriteLine("=== PDF Meta Information ===");
            Console.WriteLine($"Title   : {fileInfo.Title}");
            Console.WriteLine($"Author  : {fileInfo.Author}");
            Console.WriteLine($"Subject : {fileInfo.Subject}");
            Console.WriteLine($"Keywords: {fileInfo.Keywords}");
            Console.WriteLine($"Pages   : {fileInfo.NumberOfPages}");
            Console.WriteLine($"Encrypted: {fileInfo.IsEncrypted}");
            Console.WriteLine();
            totalPages = fileInfo.NumberOfPages; // store for later use if needed
        }

        // -------------------------------------------------
        // 2. Extract text and images using PdfExtractor
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor facade
            extractor.BindPdf(inputPdfPath);

            // Optional: set page range. If not set, the extractor processes the whole document.
            // StartPage defaults to 1 and EndPage defaults to 0 (meaning all pages).
            // Here we explicitly set the start page and rely on the default end page to process all pages.
            extractor.StartPage = 1;
            // No need to set EndPage because the API no longer exposes a PageCount property.
            // If you need to limit to a specific range, assign a concrete page number.

            // -----------------
            // Extract Text
            // -----------------
            extractor.ExtractText();
            // Save extracted text to a file
            extractor.GetText(extractedTextPath);
            Console.WriteLine($"Text extracted to: {extractedTextPath}");

            // -----------------
            // Extract Images
            // -----------------
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                // Retrieve the next image and save it as PNG
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                imageIndex++;
            }

            if (imageIndex == 1)
            {
                Console.WriteLine("No images were found in the PDF.");
            }
        }

        Console.WriteLine("PDF processing completed.");
    }
}
