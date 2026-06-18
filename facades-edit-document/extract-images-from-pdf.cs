using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Optional: extract only images actually used on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Build output file name (e.g., image-1.png, image-2.png, ...)
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image in PNG format
                extractor.GetNextImage(outputPath, ImageFormat.Png);

                Console.WriteLine($"Extracted image {imageIndex} to: {outputPath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}