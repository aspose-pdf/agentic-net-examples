using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor is a Facade that implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Set page range: 1 to 0 means all pages
            extractor.StartPage = 1;
            extractor.EndPage   = 0;

            // Perform image extraction for the specified range
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                // Save each image; default format is used (can specify ImageFormat if needed)
                extractor.GetNextImage(outPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}