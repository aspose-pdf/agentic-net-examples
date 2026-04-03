using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        // Use PdfExtractor (facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Prepare extractor for images (default mode extracts all defined resources)
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique PNG file name for each image
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Save the current image as PNG
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("All raster images have been extracted to PNG files.");
    }
}