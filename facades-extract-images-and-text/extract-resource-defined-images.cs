using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a PdfExtractor instance and bind the PDF file
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            // Explicitly set the extraction mode to DefinedInResources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Extract images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it as PNG
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(outPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}