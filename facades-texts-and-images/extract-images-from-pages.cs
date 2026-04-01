using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPdfPath = "input.pdf";
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a temporary directory for extracted pages and images
        string tempRoot = Path.Combine(Path.GetTempPath(), "PdfImageExtraction");
        Directory.CreateDirectory(tempRoot);
        string tempPdfPath = Path.Combine(tempRoot, "pages-2-5.pdf");

        // Extract pages 2 through 5 into a temporary PDF
        PdfFileEditor pageEditor = new PdfFileEditor();
        bool extractSuccess = pageEditor.Extract(inputPdfPath, 2, 5, tempPdfPath);
        if (!extractSuccess)
        {
            Console.Error.WriteLine("Failed to extract pages 2-5 from the source PDF.");
            return;
        }
        Console.WriteLine($"Pages 2-5 extracted to temporary PDF: {tempPdfPath}");

        // Extract images from the temporary PDF
        PdfExtractor imageExtractor = new PdfExtractor();
        imageExtractor.BindPdf(tempPdfPath);
        imageExtractor.ExtractImage();

        int imageIndex = 1;
        while (imageExtractor.HasNextImage())
        {
            string imageFileName = $"image-{imageIndex}.png";
            string imagePath = Path.Combine(tempRoot, imageFileName);
            bool saved = imageExtractor.GetNextImage(imagePath);
            if (saved)
            {
                Console.WriteLine($"Extracted image {imageIndex}: {imagePath}");
            }
            else
            {
                Console.Error.WriteLine($"Failed to save image {imageIndex}.");
            }
            imageIndex++;
        }

        // Clean up temporary PDF (optional)
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary PDF: {ex.Message}");
        }

        Console.WriteLine("Image extraction completed.");
    }
}