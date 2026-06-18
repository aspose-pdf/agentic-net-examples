using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (IDisposable) inside a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Prepare extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image as PNG; overload returns bool indicating success
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}