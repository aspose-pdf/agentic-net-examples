using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Folder where extracted images will be saved
        const string imagesFolder = "extracted_images";

        // Output HTML gallery file
        const string htmlPath = "gallery.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // Extract images using PdfExtractor (Facade API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(imagesFolder, $"image-{imageIndex}.png");
                // Save each image as PNG
                extractor.GetNextImage(imageFile, ImageFormat.Png);
                imageIndex++;
            }
        }

        // Build a simple HTML gallery referencing the extracted images
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
        htmlBuilder.AppendLine("    <title>PDF Image Gallery</title>");
        htmlBuilder.AppendLine("    <style>");
        htmlBuilder.AppendLine("        img { margin:5px; max-width:200px; height:auto; }");
        htmlBuilder.AppendLine("    </style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Extracted Images</h1>");

        // Add an <img> tag for each extracted image file
        foreach (string filePath in Directory.GetFiles(imagesFolder, "*.png"))
        {
            string fileName = Path.GetFileName(filePath);
            htmlBuilder.AppendLine($"    <img src=\"{imagesFolder}/{fileName}\" alt=\"{fileName}\" />");
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML content to the output file
        File.WriteAllText(htmlPath, htmlBuilder.ToString());

        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'.");
        Console.WriteLine($"HTML gallery generated at '{htmlPath}'.");
    }
}