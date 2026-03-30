using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPattern = "image-{0}.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            // Set extraction mode to retrieve images defined in resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
            // Optional: set resolution for extracted images
            extractor.Resolution = 150;
            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = string.Format(outputPattern, imageIndex);
                extractor.GetNextImage(outputPath, ImageFormat.Png);
                Console.WriteLine($"Saved image {imageIndex} to {outputPath}");
                imageIndex++;
            }
        }
    }
}