using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // <-- required for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "ExtractedImages";   // folder for extracted images
        const int pageNumber = 2;                     // page to extract from (1‑based)

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to pull images from a single page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Limit extraction range to the desired page
            extractor.StartPage = pageNumber;   // start page (inclusive)
            extractor.EndPage   = pageNumber;   // end page (inclusive)

            // Perform image extraction for the specified range
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all images found on that page
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(
                    outputDir,
                    $"page{pageNumber}_image{imageIndex}.jpg");

                // Save each image as JPEG (default format)
                extractor.GetNextImage(outPath, ImageFormat.Jpeg);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
