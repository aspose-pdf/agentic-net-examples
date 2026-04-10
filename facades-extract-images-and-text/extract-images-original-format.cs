using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images in their original format
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Set extraction mode to extract images defined in resources (original format)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image
            while (extractor.HasNextImage())
            {
                // Build output file path (extension is determined by the original image format)
                string outPath = Path.Combine(outputDir, $"image-{imageIndex}");
                // GetNextImage without specifying format preserves the original image format
                extractor.GetNextImage(outPath);
                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to '{outputDir}'.");
    }
}