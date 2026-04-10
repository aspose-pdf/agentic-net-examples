using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract embedded images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set resolution (dpi) for extracted images.
            // Higher DPI retains more detail; adjust as needed.
            // Aspose.Pdf.PdfExtractor expects an integer DPI value, not a Resolution object.
            extractor.Resolution = 300;

            // Extract images from the PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"image_{imageIndex}.bmp");

                // Save each image as BMP, preserving original color depth
                extractor.GetNextImage(outPath, ImageFormat.Bmp);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
