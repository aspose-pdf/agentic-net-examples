using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(inputPdf);

            // Restrict extraction to page 2 only.
            extractor.StartPage = 2;
            extractor.EndPage   = 2;

            // Prepare for image extraction.
            extractor.ExtractImage();

            // Retrieve the first image on page 2, if any.
            if (extractor.HasNextImage())
            {
                // Save the image as PNG.
                bool success = extractor.GetNextImage(outputPng, ImageFormat.Png);
                if (success)
                {
                    Console.WriteLine($"Image extracted to '{outputPng}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to extract the image.");
                }
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}