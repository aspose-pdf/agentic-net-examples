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
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 2; // page numbers are 1‑based
            extractor.EndPage   = 2;
            extractor.ExtractImage();

            // Retrieve the first image on page 2 and save it as PNG
            if (extractor.HasNextImage())
            {
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