using System;
using System.IO;
using System.Drawing.Imaging;               // Added for ImageFormat
using Aspose.Pdf.Facades;   // PdfExtractor
using Aspose.Pdf;          // ExtractImageMode enum (optional)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (implements IDisposable) inside a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Restrict extraction to pages 5 through 10 (1‑based indexing)
            extractor.StartPage = 5;
            extractor.EndPage   = 10;

            // Optional: extract only images actually rendered on the page
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor to pull images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Save the current image as PNG using System.Drawing.Imaging.ImageFormat
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }

            // Close is called automatically by the using statement, but explicit call is harmless
            extractor.Close();
        }

        Console.WriteLine("Image extraction completed.");
    }
}
