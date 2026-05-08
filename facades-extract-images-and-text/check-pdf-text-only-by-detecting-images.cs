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

        // Use PdfExtractor (Facade) to check for images in the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // Perform image extraction (required before querying HasNextImage)
            extractor.ExtractImage();

            // HasNextImage returns true if at least one image is present
            bool hasImages = extractor.HasNextImage();

            if (hasImages)
            {
                Console.WriteLine("The PDF contains images; it is not text‑only.");
            }
            else
            {
                Console.WriteLine("The PDF is text‑only (no images found).");
            }
        }
    }
}