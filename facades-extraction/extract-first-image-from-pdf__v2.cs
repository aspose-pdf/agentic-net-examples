using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagePath = "FirstImage.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor to extract images. The modern API provides HasNextImage() and GetNextImage()
        // instead of the removed GetImageCount() / GetImage() methods.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document.
            extractor.BindPdf(pdfPath);

            // (Optional) Restrict extraction to the first page only.
            extractor.StartPage = 1;
            extractor.EndPage   = 1;

            // Extract images from the bound PDF.
            extractor.ExtractImage();

            // Check if any image was found.
            if (!extractor.HasNextImage())
            {
                Console.WriteLine("No images found in the PDF.");
                return;
            }

            // Save the first extracted image as PNG.
            // GetNextImage can write directly to a file and accepts an ImageFormat.
            extractor.GetNextImage(imagePath, ImageFormat.Png);
        }

        Console.WriteLine($"First image extracted and saved to '{imagePath}'.");
    }
}
