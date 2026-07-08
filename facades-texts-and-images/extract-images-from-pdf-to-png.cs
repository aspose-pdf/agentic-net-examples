using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (implements IDisposable) within a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageCounter = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build the output file path for each PNG image
                string outputImagePath = Path.Combine(outputFolder, $"image-{imageCounter}.png");

                // Save the current image as PNG; GetNextImage returns a bool which we can ignore here
                extractor.GetNextImage(outputImagePath, ImageFormat.Png);

                imageCounter++;
            }
        }

        Console.WriteLine("All images have been extracted to PNG files.");
    }
}