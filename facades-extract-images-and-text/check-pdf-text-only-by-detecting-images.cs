using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be examined
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to work with the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract images from the document (required before calling HasNextImage)
            extractor.ExtractImage();

            // Determine whether any images are present
            bool hasImages = extractor.HasNextImage();

            if (hasImages)
            {
                Console.WriteLine("The PDF contains images (not text‑only).");
            }
            else
            {
                Console.WriteLine("The PDF is text‑only (no images were found).");
            }
        }
    }
}