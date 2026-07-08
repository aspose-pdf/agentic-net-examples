using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file containing images
        const string inputPdf = "sample.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (a Facade) to extract images from the PDF.
        // The extractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdf);

            // Set the page range.
            // StartPage = 1 (first page), EndPage = 0 means "all pages".
            extractor.StartPage = 1;
            extractor.EndPage   = 0;

            // Perform the image extraction.
            extractor.ExtractImage();

            // Retrieve each extracted image and save it to a separate file.
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG. You can change the format if needed.
                string outputImage = $"image-{imageIndex}.png";
                extractor.GetNextImage(outputImage, ImageFormat.Png);
                Console.WriteLine($"Extracted image saved to: {outputImage}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}