using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page2_image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (Facade) to extract images from a specific page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Limit extraction to page 2 (Aspose.Pdf uses 1‑based indexing)
            extractor.StartPage = 2;
            extractor.EndPage   = 2;

            // Perform image extraction for the selected page range
            extractor.ExtractImage();

            // Retrieve the first image, if any, and save it as PNG
            if (extractor.HasNextImage())
            {
                // GetNextImage returns true on success
                bool saved = extractor.GetNextImage(outputPng, ImageFormat.Png);
                if (saved)
                {
                    Console.WriteLine($"First image from page 2 saved to '{outputPng}'.");
                }
                else
                {
                    Console.WriteLine("Failed to save the extracted image.");
                }
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}