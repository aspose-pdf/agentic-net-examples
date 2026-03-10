using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for extracted content
        const string outputDir = "ExtractedContent";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Paths for extracted text and images
        string textOutputPath = Path.Combine(outputDir, "extracted_text.txt");

        // Verify that the source PDF exists before attempting extraction
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: The file '{inputPdfPath}' was not found. Please provide a valid PDF file.");
            return;
        }

        // Use PdfExtractor facade to extract text and images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document (can also bind from a stream if needed)
            extractor.BindPdf(inputPdfPath);

            // -------------------------
            // Extract textual content
            // -------------------------
            extractor.ExtractText();                     // Prepare text extraction
            extractor.GetText(textOutputPath);           // Save extracted text to a file

            // -------------------------
            // Extract images
            // -------------------------
            extractor.ExtractImage();                    // Prepare image extraction

            int imageIndex = 1;
            // Suppress the platform‑specific warning for ImageFormat.Png (CA1416)
#pragma warning disable CA1416 // Validate platform compatibility
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Retrieve the next image and save it as PNG
                extractor.GetNextImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                imageIndex++;
            }
#pragma warning restore CA1416 // Validate platform compatibility
        }

        Console.WriteLine("Extraction completed.");
        Console.WriteLine($"Text saved to: {textOutputPath}");
        Console.WriteLine($"Images saved to folder: {outputDir}");
    }
}
