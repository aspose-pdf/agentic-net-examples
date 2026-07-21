using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";
        const int pageNumber = 3; // page to extract images from

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images from a single page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Limit extraction to the desired page
            extractor.StartPage = pageNumber;
            extractor.EndPage   = pageNumber;

            // Extract images from the bound page range
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image as JPEG
                string outputPath = Path.Combine(outputDir,
                    $"page{pageNumber}_image{imageIndex}.jpg");
                extractor.GetNextImage(outputPath, ImageFormat.Jpeg);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}