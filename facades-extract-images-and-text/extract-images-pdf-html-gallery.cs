using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging; // ImageFormat for saving images

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesDir = "extracted_images";
        const string htmlPath = "gallery.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output folder for images exists
        Directory.CreateDirectory(imagesDir);

        // -------- Extract images from the PDF --------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Use the default extraction mode (images defined in resources)
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Initialize the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each extracted image
                string imageFile = Path.Combine(imagesDir, $"image_{imageIndex}.png");

                // Save the image as PNG (you can choose other formats via ImageFormat)
                extractor.GetNextImage(imageFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        // -------- Generate a simple HTML gallery --------
        using (StreamWriter writer = new StreamWriter(htmlPath, false))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"UTF-8\">");
            writer.WriteLine("    <title>Extracted Images Gallery</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <h1>Extracted Images</h1>");
            writer.WriteLine("    <div style=\"display:flex; flex-wrap:wrap; gap:10px;\">");

            // Add an <img> tag for each extracted image file
            foreach (string filePath in Directory.GetFiles(imagesDir))
            {
                // Use forward slashes for web compatibility
                string relativePath = Path.Combine(imagesDir, Path.GetFileName(filePath)).Replace("\\", "/");
                writer.WriteLine($"        <img src=\"{relativePath}\" style=\"max-width:200px; height:auto;\" />");
            }

            writer.WriteLine("    </div>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Image extraction completed. HTML gallery saved to '{htmlPath}'.");
    }
}