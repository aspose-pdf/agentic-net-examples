using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class PdfExtractDemo
{
    static void Main(string[] args)
    {
        // Input PDF path (adjust as needed)
        const string inputPdfPath = "input.pdf";

        // Output folder for extracted images
        const string imagesOutputFolder = "ExtractedImages";

        // Output text file path
        const string extractedTextPath = "ExtractedText.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputFolder);

        try
        {
            // ---------- Create and load ----------
            // Create a PdfExtractor instance
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // ---------- Extract images ----------
            // Extract images that are actually shown on the pages
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Save each extracted image to a separate file
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(imagesOutputFolder, $"Image_{imageIndex}.png");
                extractor.GetNextImage(imageFile);
                Console.WriteLine($"Saved image: {imageFile}");
                imageIndex++;
            }

            // ---------- Extract text ----------
            // Extract text from the whole document
            extractor.ExtractText();

            // Write the extracted text directly to a file (overload requires a file path)
            extractor.GetText(extractedTextPath);
            Console.WriteLine($"Extracted text saved to: {extractedTextPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
