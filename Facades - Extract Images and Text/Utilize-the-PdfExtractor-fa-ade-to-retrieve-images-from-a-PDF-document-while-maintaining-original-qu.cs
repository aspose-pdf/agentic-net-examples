using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor facade to pull images out of the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract only images that are actually rendered on pages (optional but common)
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Increase resolution to preserve original quality (default is 150 DPI)
            extractor.Resolution = 300; // 300 DPI gives higher fidelity

            // Perform the extraction operation
            extractor.ExtractImage();

            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for each extracted image
                string outPath = Path.Combine(outputDir, $"image-{imgIndex}.png");

                // GetNextImage saves the image; overload without ImageFormat keeps the original format
                // (if you need a specific format, use the overload with ImageFormat)
                extractor.GetNextImage(outPath);

                imgIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputDir}'.");
    }
}