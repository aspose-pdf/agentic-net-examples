using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        // Temporary directory for extracted pages and images
        string tempDir = Path.Combine(Path.GetTempPath(), "pdf_images_temp");
        Directory.CreateDirectory(tempDir);

        // PDF containing only pages 2 through 5
        string extractedPdfPath = Path.Combine(tempDir, "pages2to5.pdf");

        // Extract pages 2-5 using PdfFileEditor
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool pagesExtracted = fileEditor.Extract(inputPath, 2, 5, extractedPdfPath);
        if (!pagesExtracted)
        {
            Console.Error.WriteLine("Failed to extract the required pages.");
            return;
        }

        // Extract images from the newly created PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(extractedPdfPath);
        extractor.ExtractImage();
        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string imagePath = Path.Combine(tempDir, $"image-{imageIndex}.png");
            // Save the image in its original format (no System.Drawing dependency)
            bool imageSaved = extractor.GetNextImage(imagePath);
            if (!imageSaved)
            {
                Console.Error.WriteLine($"Failed to save image {imageIndex}.");
                break;
            }
            imageIndex++;
        }

        Console.WriteLine($"Extracted {imageIndex - 1} image(s) to '{tempDir}'.");
    }
}
