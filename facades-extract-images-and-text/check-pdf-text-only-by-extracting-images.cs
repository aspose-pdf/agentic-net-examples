using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfExtractor to determine if the PDF contains any images.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPath);

            // Must call ExtractImage before checking for images.
            extractor.ExtractImage();

            int imageCount = 0;
            while (extractor.HasNextImage())
            {
                // Retrieve each image into a memory stream (no file output needed).
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                }
                imageCount++;
            }

            bool isTextOnly = imageCount == 0;
            Console.WriteLine(isTextOnly
                ? "The PDF is text‑only (no images found)."
                : $"The PDF contains {imageCount} image(s).");
        }
    }
}