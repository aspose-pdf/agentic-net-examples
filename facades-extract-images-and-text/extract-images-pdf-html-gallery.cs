using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string imagesFolder = "images";        // folder for extracted images
        const string htmlPath = "gallery.html";      // output HTML gallery

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // -------------------------------------------------
        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        // -------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);          // load the PDF
        extractor.ExtractImage();            // prepare image extraction

        int imgIndex = 1;
        StringBuilder html = new StringBuilder();

        // Start building a simple HTML page
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head><meta charset=\"UTF-8\"><title>Image Gallery</title></head>");
        html.AppendLine("<body>");
        html.AppendLine("<h1>Extracted Images</h1>");
        html.AppendLine("<div style=\"display:flex;flex-wrap:wrap;gap:10px;\">");

        // Loop through all extracted images
        while (extractor.HasNextImage())
        {
            // Save each image as PNG (you can choose other formats)
            string imgFileName = $"image-{imgIndex}.png";
            string imgPath = Path.Combine(imagesFolder, imgFileName);
            extractor.GetNextImage(imgPath, System.Drawing.Imaging.ImageFormat.Png);

            // Add an <img> tag to the HTML (use forward slashes for web paths)
            string webPath = Path.Combine(imagesFolder, imgFileName).Replace("\\", "/");
            html.AppendLine($"<div><img src=\"{webPath}\" alt=\"Image {imgIndex}\" style=\"max-width:200px;\"/></div>");

            imgIndex++;
        }

        // Finish HTML
        html.AppendLine("</div>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML file
        File.WriteAllText(htmlPath, html.ToString(), Encoding.UTF8);

        Console.WriteLine($"Extracted {imgIndex - 1} images to '{imagesFolder}' and generated gallery at '{htmlPath}'.");
    }
}