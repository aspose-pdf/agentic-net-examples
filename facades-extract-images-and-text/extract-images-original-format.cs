using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfExtractor resides here
using Aspose.Pdf;           // ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (implements IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Set extraction mode to keep the original image format
            // DefinedInResources extracts images exactly as stored in the PDF resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file path; the extractor will preserve the original format
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}");

                // Save the current image; the method determines the correct file extension
                extractor.GetNextImage(outputPath);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}