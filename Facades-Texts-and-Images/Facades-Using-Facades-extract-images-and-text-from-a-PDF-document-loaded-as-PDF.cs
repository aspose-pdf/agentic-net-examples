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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // PdfExtractor implements IDisposable, so use a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // ---------- Extract Text ----------
                extractor.ExtractText();                     // Perform the extraction
                extractor.GetText(textOutputPath);           // Save all text to a single file
                Console.WriteLine($"Text extracted to: {textOutputPath}");

                // ---------- Extract Images ----------
                extractor.ExtractImage();                    // Prepare image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each image using its original format
                    string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Image saved: {imagePath}");
                    imageIndex++;
                }

                // No explicit PreSave or Close needed; using block disposes the extractor
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}