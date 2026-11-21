using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // The ExtractImageMode property does not exist in the current Aspose.Pdf.Facades version.
            // Image extraction is performed by calling ExtractImage(), which extracts all images by default.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // <-- removed for compatibility

            // Extract all images from the PDF
            extractor.ExtractImage();

            // Save each extracted image to the output directory
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
