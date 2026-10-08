using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string imagesFolder   = "ExtractedImages";
        const string outputHtmlPath = "gallery.html";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            while (true)
            {
                // Build a file name for the next image
                string imagePath = Path.Combine(imagesFolder, $"image_{imageIndex}.png");

                // GetNextImage returns true if an image was written, false otherwise
                bool hasImage = extractor.GetNextImage(imagePath);
                if (!hasImage)
                    break; // No more images

                Console.WriteLine($"Extracted: {imagePath}");
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // Generate a simple HTML gallery referencing the extracted images
        // -----------------------------------------------------------------
        StringBuilder htmlBuilder = new StringBuilder();

        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html lang=\"en\">");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
        htmlBuilder.AppendLine("    <title>PDF Image Gallery</title>");
        htmlBuilder.AppendLine("    <style>");
        htmlBuilder.AppendLine("        body { font-family: Arial, sans-serif; }");
        htmlBuilder.AppendLine("        .gallery { display: flex; flex-wrap: wrap; gap: 10px; }");
        htmlBuilder.AppendLine("        .gallery img { max-width: 200px; height: auto; border: 1px solid #ccc; }");
        htmlBuilder.AppendLine("    </style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Extracted Images</h1>");
        htmlBuilder.AppendLine("    <div class=\"gallery\">");

        // Add an <img> tag for each extracted image file
        foreach (string imgFile in Directory.GetFiles(imagesFolder, "image_*.png"))
        {
            string relativePath = Path.GetFileName(imgFile);
            htmlBuilder.AppendLine($"        <img src=\"{relativePath}\" alt=\"Extracted image\" />");
        }

        htmlBuilder.AppendLine("    </div>");
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML file (saved in the same folder as the images)
        File.WriteAllText(outputHtmlPath, htmlBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"HTML gallery generated: {outputHtmlPath}");
    }
}