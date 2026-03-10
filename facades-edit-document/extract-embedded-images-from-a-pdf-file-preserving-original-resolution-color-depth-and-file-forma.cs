using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for extraction
using Aspose.Pdf;          // For ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract only images that are actually rendered on pages (optional but common)
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Set a high resolution to keep rasterized images sharp.
            // This does not affect vector images or already embedded bitmap formats.
            extractor.Resolution = 300;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name; the extractor will preserve the original image format.
                string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}");

                // Retrieve the next image. No format is specified, so the original format is kept.
                // The method returns a bool indicating success; we ignore it here.
                extractor.GetNextImage(outputPath);

                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Images saved to \"{outputFolder}\".");
    }
}