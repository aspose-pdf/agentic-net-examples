using System;
using System.IO;
using Aspose.Pdf;                 // For ExtractImageMode enum
using Aspose.Pdf.Facades;        // PdfExtractor facade
using System.Drawing.Imaging;    // ImageFormat for PNG output

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Validate input file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor within a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Optional: extract only images actually shown on pages
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Prepare the extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Build the output file name (PNG format)
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Save the current image as PNG
                extractor.GetNextImage(imagePath, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine($"All images have been extracted to '{outputFolder}'.");
    }
}