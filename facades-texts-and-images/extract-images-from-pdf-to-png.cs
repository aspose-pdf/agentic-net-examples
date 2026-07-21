using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor resides here
using System.Drawing.Imaging;      // ImageFormat for PNG

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use using for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                // Save the current image as PNG; GetNextImage returns true on success
                bool saved = extractor.GetNextImage(outPath, ImageFormat.Png);
                if (!saved)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted.");
    }
}