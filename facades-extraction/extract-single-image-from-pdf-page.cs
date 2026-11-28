using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJpg = "page3_image.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable; use using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 3;
            extractor.EndPage   = 3;

            // Enable image extraction
            extractor.ExtractImage();

            // Extract images from the specified page and save the first one as JPEG
            if (extractor.HasNextImage())
            {
                extractor.GetNextImage(outputJpg);
                Console.WriteLine($"Image extracted and saved to '{outputJpg}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 3.");
            }
        }
    }
}