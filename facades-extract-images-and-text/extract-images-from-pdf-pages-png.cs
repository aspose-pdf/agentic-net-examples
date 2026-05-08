using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for PNG

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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PDF extractor and bind the source PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);

        // Restrict extraction to pages 5 through 10 (1‑based indexing)
        extractor.StartPage = 5;
        extractor.EndPage = 10;

        // Perform image extraction for the specified page range
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            // Build a unique file name for each extracted image
            string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

            // Save the image as PNG
            extractor.GetNextImage(outPath, ImageFormat.Png);
            imageIndex++;
        }

        // Release resources held by the extractor
        extractor.Close();

        Console.WriteLine($"Extraction complete. {imageIndex - 1} images saved to '{outputDir}'.");
    }
}