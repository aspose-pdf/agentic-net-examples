using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string outputDir = "ExtractedImages";  // folder for images

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so a using block guarantees disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Save each image as a separate file (default format is PDF)
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.pdf");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}