using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf;                           // ExtractImageMode enum
using Aspose.Pdf.Facades;                  // PdfExtractor facade

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Set extraction mode to retrieve all images defined in resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Save each image as PNG (you can choose other formats if desired)
                extractor.GetNextImage(outPath, ImageFormat.Png);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}