using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";          // source PDF
        const string outputDir = "ExtractedImages";     // folder for extracted images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Set extraction mode to retrieve images exactly as they are stored in the PDF
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build output file name (original format is preserved by this overload)
                string outputPath = Path.Combine(outputDir, $"image_{imageIndex}");

                // The GetNextImage(string) overload saves the image in its original format.
                // It automatically appends the appropriate file extension.
                extractor.GetNextImage(outputPath);

                Console.WriteLine($"Extracted image {imageIndex} to {outputPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}