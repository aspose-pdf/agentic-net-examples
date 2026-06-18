using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where extracted BMP images will be saved
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract images from the document.
            // This extracts the images as they are stored in the PDF,
            // preserving original resolution and color depth.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build the output BMP file name
                string outputPath = Path.Combine(outputDir, $"image{imageIndex}.bmp");

                // Save the next image as BMP.
                // The overload with ImageFormat preserves the original image data.
                extractor.GetNextImage(outputPath, ImageFormat.Bmp);

                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. BMP files saved to '{outputDir}'.");
    }
}
