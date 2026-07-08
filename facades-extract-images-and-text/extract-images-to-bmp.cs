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

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Keep default resolution (original image resolution is preserved)
            // If you need to change it, set extractor.Resolution = new Resolution(dpi);

            // Extract images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build output BMP file path
                string outPath = Path.Combine(outputDir, $"image_{imageIndex}.bmp");

                // Save the current image as BMP, preserving its original color depth
                extractor.GetNextImage(outPath, ImageFormat.Bmp);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted as BMP files.");
    }
}
