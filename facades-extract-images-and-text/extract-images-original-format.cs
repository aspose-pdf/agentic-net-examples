using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the extractor and bind the PDF document
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);

        // Configure extraction to retrieve images that are actually used on pages
        // (images are saved in their original format)
        extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

        // Start the image extraction process
        extractor.ExtractImage();

        int imageIndex = 1;
        // Retrieve each extracted image and save it
        while (extractor.HasNextImage())
        {
            string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}");
            // GetNextImage(string) preserves the original image format,
            // so we omit a specific extension.
            extractor.GetNextImage(outputPath);
            imageIndex++;
        }

        // Release resources
        extractor.Close();

        Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
    }
}