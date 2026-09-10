using System;
using System.IO;
using Aspose.Pdf;
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

        // PdfExtractor implements IDisposable, so use a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file.
            extractor.BindPdf(inputPdf);

            // Set the extraction mode to retrieve images as they are stored in the PDF.
            // This keeps the original image format (e.g., JPEG, PNG, etc.).
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Perform the extraction.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // GetNextImage without specifying an ImageFormat preserves the original format.
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}.img");
                bool success = extractor.GetNextImage(outputPath);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image {imageIndex}");
                }
                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputDir}'.");
    }
}