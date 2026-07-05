using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Optional: extract only images actually used on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the image as PNG (default format is JPEG if not specified)
                extractor.GetNextImage(imagePath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine($"All images extracted to folder: {outputFolder}");
    }
}