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

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (facade) to pull images out of the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images found in the document
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each extracted image
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image as PNG (other formats are also supported)
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}