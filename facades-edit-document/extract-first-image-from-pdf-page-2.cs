using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page2_image1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor to extract images from a specific page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Restrict extraction to page 2 (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 2;
            extractor.EndPage   = 2;

            // Extract images from the bound page range
            extractor.ExtractImage();

            // If at least one image exists, save the first one as PNG
            if (extractor.HasNextImage())
            {
                // GetNextImage returns a bool indicating success; we ignore it here
                extractor.GetNextImage(outputPng, ImageFormat.Png);
                Console.WriteLine($"First image from page 2 saved as '{outputPng}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}