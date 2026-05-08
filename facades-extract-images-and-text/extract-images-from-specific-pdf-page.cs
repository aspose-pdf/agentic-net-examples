using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExtractedImages";    // folder for extracted images
        const int  pageNumber = 2;                     // page to extract images from (1‑based)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor is a Facade; it implements IDisposable, so use a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Limit extraction to a single page
            extractor.StartPage = pageNumber;
            extractor.EndPage   = pageNumber;

            // Extract images from the specified page
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build output file name: page{page}_image{index}.png
                string outputPath = Path.Combine(
                    outputDir,
                    $"page{pageNumber}_image{imageIndex}.png");

                // Save the next image in PNG format
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
