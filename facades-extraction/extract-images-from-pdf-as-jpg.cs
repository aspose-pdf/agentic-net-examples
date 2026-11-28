using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPath);

            // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // Extraction of images is performed by calling ExtractImage().
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Save each image as a JPG file with a sequential name.
                string outPath = Path.Combine(outputDir, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(outPath, ImageFormat.Jpeg);
                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and saved as JPG files.");
    }
}
