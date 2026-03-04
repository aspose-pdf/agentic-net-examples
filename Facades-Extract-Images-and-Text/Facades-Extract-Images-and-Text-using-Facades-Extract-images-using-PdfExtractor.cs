using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";          // PDF to process
        const string outputTextPath = "extracted.txt";    // Text output file
        const string imageOutputPattern = "image-{0}.png"; // Image file pattern

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor facade.
                extractor.BindPdf(inputPdfPath);

                // -------------------------
                // Extract all images.
                // -------------------------
                extractor.ExtractImage(); // Prepare image extraction.

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each image to a separate file.
                    string imagePath = string.Format(imageOutputPattern, imageIndex);
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} → {imagePath}");
                    imageIndex++;
                }

                // -------------------------
                // Extract all text.
                // -------------------------
                extractor.ExtractText(); // Prepare text extraction.

                // Save the extracted text to a file.
                extractor.GetText(outputTextPath);
                Console.WriteLine($"Extracted text saved to '{outputTextPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}