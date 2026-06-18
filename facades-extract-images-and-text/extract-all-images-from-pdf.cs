using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat enum for specifying output format

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (implements IDisposable) within a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Set page range: StartPage = 1, EndPage = 0 (means all pages)
            extractor.StartPage = 1;
            extractor.EndPage = 0;

            // Perform image extraction for the defined range
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Save each image as PNG (any ImageFormat can be used)
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}