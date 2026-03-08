using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "Extracted";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // -------------------------
            // Extract images from PDF
            // -------------------------
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image as PNG (default format)
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }

            // -------------------------
            // Extract text from PDF
            // -------------------------
            extractor.ExtractText();

            // Save all extracted text to a single file
            string textPath = Path.Combine(outputDir, "full-text.txt");
            extractor.GetText(textPath);

            // Optional: extract per‑page text into separate files
            /*
            int pageNum = 1;
            while (extractor.HasNextPageText())
            {
                string pageTextPath = Path.Combine(outputDir, $"page-{pageNum}.txt");
                extractor.GetNextPageText(pageTextPath);
                pageNum++;
            }
            */
        }

        Console.WriteLine("Extraction of images and text completed.");
    }
}