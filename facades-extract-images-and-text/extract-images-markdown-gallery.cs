using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagesDir = "images";
        const string markdownPath = "gallery.md";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesDir);

        StringBuilder markdown = new StringBuilder();
        markdown.AppendLine("# Image Gallery");
        markdown.AppendLine();

        // Extract images using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(imagesDir, $"image-{imageIndex}.jpg");
                extractor.GetNextImage(imageFile); // Saves the next image as JPEG
                markdown.AppendLine($"![]({imageFile})");
                imageIndex++;
            }
        }

        // Write the markdown file
        File.WriteAllText(markdownPath, markdown.ToString());
        Console.WriteLine($"Markdown gallery created at '{markdownPath}'.");
    }
}