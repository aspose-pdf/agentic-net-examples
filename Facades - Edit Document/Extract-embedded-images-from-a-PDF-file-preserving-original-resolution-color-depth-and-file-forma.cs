using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract only images that are actually used on pages (optional, improves relevance)
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Set a high resolution to keep original quality (default is 150 DPI)
            extractor.Resolution = 300; // DPI

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Use the overload that saves the image in its original format.
                // The method will automatically append the correct file extension.
                string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}");
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
