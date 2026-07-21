using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;   // PdfExtractor resides here
using Aspose.Pdf;          // ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputDir = "ExtractedImages"; // folder for PNG files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (implements IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Restrict extraction to pages 5 through 10 (1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Optional: extract only images actually rendered on the page
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file name: e.g., image-1.png, image-2.png, ...
                string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                // Save the current image as PNG
                bool success = extractor.GetNextImage(outputPath, ImageFormat.Png);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}