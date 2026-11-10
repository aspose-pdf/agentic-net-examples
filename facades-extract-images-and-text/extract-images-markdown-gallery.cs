using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string imagesFolder  = "extracted_images";
        const string markdownFile  = "gallery.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // Extract images from the PDF using PdfExtractor (Aspose.Pdf.Facades)
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image directly to a file.
                // Here we choose PNG as a universal format.
                string imagePath = Path.Combine(imagesFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }
        }

        // ---------------------------------------------------------------
        // Generate a Markdown gallery that references the extracted images
        // ---------------------------------------------------------------
        using (StreamWriter writer = new StreamWriter(markdownFile, false))
        {
            writer.WriteLine("# Image Gallery");
            writer.WriteLine();

            string[] imageFiles = Directory.GetFiles(imagesFolder);
            foreach (string filePath in imageFiles)
            {
                string fileName = Path.GetFileName(filePath);
                // Use forward slashes for Markdown compatibility
                string relativePath = Path.Combine(imagesFolder, fileName).Replace("\\", "/");
                writer.WriteLine($"![{fileName}]({relativePath})");
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Images extracted to '{imagesFolder}'.");
        Console.WriteLine($"Markdown gallery created at '{markdownFile}'.");
    }

    // Helper method retained for potential future use; currently unused.
    static string GetExtensionFromFormat(ImageFormat format)
    {
        if (format.Equals(ImageFormat.Png))   return ".png";
        if (format.Equals(ImageFormat.Jpeg))  return ".jpg";
        if (format.Equals(ImageFormat.Gif))   return ".gif";
        if (format.Equals(ImageFormat.Bmp))   return ".bmp";
        if (format.Equals(ImageFormat.Tiff))  return ".tiff";
        // Fallback for uncommon formats
        return ".img";
    }
}
