using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "images";
        const string htmlOutputPath = "gallery.html";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Create folder for extracted images
        Directory.CreateDirectory(imagesFolder);

        // Extract images using PdfExtractor (Facade API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);   // Load the PDF
            extractor.ExtractImage();          // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for each image
                string imageFileName = $"image-{imageIndex}.png";
                string imagePath = Path.Combine(imagesFolder, imageFileName);

                // Save the next image (default format – PNG works for most cases)
                // GetNextImage returns true on success; ignore the return value here
                extractor.GetNextImage(imagePath);

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
        htmlBuilder.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
        htmlBuilder.AppendLine("        .gallery-item { margin-bottom: 20px; }");
        htmlBuilder.AppendLine("        img { max-width: 100%; height: auto; border: 1px solid #ccc; }");
        htmlBuilder.AppendLine("    </style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Extracted Images</h1>");

        // Add an <img> tag for each extracted image file
        foreach (string imageFilePath in Directory.GetFiles(imagesFolder))
        {
            string fileName = Path.GetFileName(imageFilePath);
            string relativePath = Path.Combine(imagesFolder, fileName).Replace("\\", "/");
            htmlBuilder.AppendLine($"    <div class=\"gallery-item\"><img src=\"{relativePath}\" alt=\"{fileName}\" /></div>");
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML file
        File.WriteAllText(htmlOutputPath, htmlBuilder.ToString());

        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'.");
        Console.WriteLine($"HTML gallery generated at '{htmlOutputPath}'.");
    }
}