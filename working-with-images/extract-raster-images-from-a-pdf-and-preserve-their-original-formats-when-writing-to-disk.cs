using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF to extract images from
        const string outputDir = "ExtractedImages";    // Folder for saved images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract all images from the document (default mode extracts all defined resources)
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each image and save it preserving its original format
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}");
                // GetNextImage(string) writes the image using its original format and appropriate extension
                extractor.GetNextImage(outputPath);
                Console.WriteLine($"Saved image {imageIndex} → {outputPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}