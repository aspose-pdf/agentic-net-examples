using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "extracted_images";
        const string markdownPath = "gallery.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Extract images using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            // Optional: set higher resolution for clearer images
            extractor.Resolution = 300;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFileName = $"image-{imageIndex}.png";
                string imagePath = Path.Combine(imagesFolder, imageFileName);
                // Save each image. Use overload without explicit ImageFormat to avoid missing enum issue.
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        // Build markdown gallery
        List<string> imageFiles = new List<string>(Directory.GetFiles(imagesFolder, "image-*.png"));
        imageFiles.Sort(); // Ensure consistent order

        using (StreamWriter writer = new StreamWriter(markdownPath, false))
        {
            writer.WriteLine("# Image Gallery");
            writer.WriteLine();

            foreach (string imageFile in imageFiles)
            {
                string fileName = Path.GetFileName(imageFile);
                // Use relative path for markdown link
                string relativePath = Path.Combine(imagesFolder, fileName).Replace('\\', '/');
                writer.WriteLine($"![{fileName}]({relativePath})");
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Extraction complete. Markdown gallery saved to '{markdownPath}'.");
    }
}
