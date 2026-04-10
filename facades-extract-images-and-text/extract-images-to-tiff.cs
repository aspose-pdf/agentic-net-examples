using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExtractedImages";    // folder for TIFF files

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

            // Tell the extractor to work on images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build a TIFF file name for each image
                string tiffPath = Path.Combine(outputDir, $"image-{imageIndex}.tiff");

                // Save the current image as TIFF (lossless)
                extractor.GetNextImage(tiffPath, ImageFormat.Tiff);

                Console.WriteLine($"Saved image {imageIndex} → {tiffPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
