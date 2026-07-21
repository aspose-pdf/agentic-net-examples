using System;
using System.IO;
using Aspose.Pdf.Facades;                     // PdfExtractor
using System.Drawing.Imaging;                // ImageFormat (BMP)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to pull out embedded images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Preserve the original image resolution and color depth.
            // Setting Resolution to 0 tells the extractor not to resample the image.
            extractor.Resolution = 0;

            // Extract images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.bmp");

                // Save the next image as BMP, keeping its native properties.
                extractor.GetNextImage(outputPath, ImageFormat.Bmp);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted as BMP files.");
    }
}