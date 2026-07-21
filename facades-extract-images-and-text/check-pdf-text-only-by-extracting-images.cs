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

        // Use PdfExtractor to examine the PDF for images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int imageCount = 0;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the image into a memory stream (no file is written)
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                }
                imageCount++;
            }

            // Determine if the PDF is text‑only
            if (imageCount == 0)
                Console.WriteLine("The PDF is text-only (no images found).");
            else
                Console.WriteLine($"The PDF contains {imageCount} image(s).");
        }
    }
}