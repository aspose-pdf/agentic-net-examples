using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPng = "page2_first_image.png"; // extracted image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (facade) to extract images.
        // Set page range to page 2 only.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 2;
            extractor.EndPage   = 2;

            // Prepare for image extraction.
            extractor.ExtractImage();

            // Retrieve the first image (if any) and save as PNG.
            if (extractor.HasNextImage())
            {
                // Save the image in PNG format.
                extractor.GetNextImage(outputPng, ImageFormat.Png);
                Console.WriteLine($"First image from page 2 saved to '{outputPng}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}