using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PdfImages";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Create the extractor (PdfExtractor does not implement IDisposable in older versions)
        var extractor = new PdfExtractor();

        // Load the PDF document
        extractor.BindPdf(inputPath);

        // Perform the image extraction operation (default extracts all images)
        extractor.ExtractImage();

        int imageIndex = 1;
        // Iterate through all extracted images
        while (extractor.HasNextImage())
        {
            string imagePath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
            extractor.GetNextImage(imagePath);
            Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
            imageIndex++;
        }

        Console.WriteLine("Image extraction completed.");
    }
}
