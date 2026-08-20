using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string imagesDir = "extracted_images";   // folder for images
        const string htmlPath  = "gallery.html";       // output HTML file

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesDir);

        // ---------- Extract images using PdfExtractor ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // NOTE: The ExtractImageMode property is not available in the
            // current Aspose.Pdf.Facades version. The default behavior extracts
            // all images, so we simply call ExtractImage().
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // removed for compatibility

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for each extracted image
                string imageFile = Path.Combine(imagesDir, $"image-{imageIndex}.png");

                // Save the image as PNG
                extractor.GetNextImage(imageFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        // ---------- Generate simple HTML gallery ----------
        StringBuilder htmlBuilder = new StringBuilder();

        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html lang=\"en\">");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
        htmlBuilder.AppendLine("    <title>Extracted Images Gallery</title>");
        htmlBuilder.AppendLine("    <style>");
        htmlBuilder.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
        htmlBuilder.AppendLine("        img { max-width: 100%; height: auto; display: block; margin-bottom: 10px; }");
        htmlBuilder.AppendLine("        .img-container { margin-bottom: 20px; }");
        htmlBuilder.AppendLine("    </style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Extracted Images</h1>");

        // List all extracted image files and add them to the HTML
        foreach (string filePath in Directory.GetFiles(imagesDir, "image-*.png"))
        {
            string fileName = Path.GetFileName(filePath);
            string relativePath = $"{imagesDir}/{fileName}";
            htmlBuilder.AppendLine("    <div class=\"img-container\">");
            htmlBuilder.AppendLine($"        <img src=\"{relativePath}\" alt=\"{fileName}\" />");
            htmlBuilder.AppendLine("    </div>");
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML content to the output file
        File.WriteAllText(htmlPath, htmlBuilder.ToString());

        Console.WriteLine($"Image extraction completed. Gallery created at '{htmlPath}'.");
    }
}
