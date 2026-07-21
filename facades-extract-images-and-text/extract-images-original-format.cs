using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfExtractor and related facades
using Aspose.Pdf;          // ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to pull images out of the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set extraction mode to keep images in their original format.
            // DefinedInResources extracts all images defined in the PDF resources.
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image using its original format (no format conversion)
                string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}");
                // GetNextImage without specifying ImageFormat preserves the original format.
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
    }
}