using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;          // for ImageFormat
using Aspose.Pdf.Facades;            // PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string imagesFolder   = "extracted_images";
        const string htmlReportPath = "report.html";

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // Extract all images from the PDF using PdfExtractor
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract images from all pages (StartPage/EndPage are 1‑based;
            // leaving them unset means the whole document)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Saved image {imageIndex} → {imagePath}");
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // Build a simple HTML report that displays the extracted images
        // -----------------------------------------------------------------
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head>");
        html.AppendLine("    <meta charset=\"UTF-8\">");
        html.AppendLine("    <title>Signature Images Report</title>");
        html.AppendLine("    <style>");
        html.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
        html.AppendLine("        img { margin: 10px 0; max-width: 100%; height: auto; border: 1px solid #ccc; }");
        html.AppendLine("    </style>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");
        html.AppendLine("    <h1>Extracted Signature Images</h1>");

        // Add each extracted image to the HTML
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.png");
        if (imageFiles.Length == 0)
        {
            html.AppendLine("    <p>No images were found in the PDF.</p>");
        }
        else
        {
            foreach (string imgFile in imageFiles)
            {
                // Use a relative path so the HTML can be opened directly
                string relativePath = Path.GetFileName(imgFile);
                html.AppendLine($"    <div><img src=\"{relativePath}\" alt=\"Extracted image\" /></div>");
            }
        }

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML file to the same folder as the images
        File.WriteAllText(htmlReportPath, html.ToString(), Encoding.UTF8);
        Console.WriteLine($"HTML report generated: {htmlReportPath}");
    }
}