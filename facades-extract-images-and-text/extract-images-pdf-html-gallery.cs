using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string imagesFolder = "images";         // folder for extracted images
        const string htmlFile = "gallery.html";       // output HTML gallery

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // ---------- Extract images using PdfExtractor ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images in the document
            while (extractor.HasNextImage())
            {
                // Build a file name for each image
                string imagePath = Path.Combine(imagesFolder, $"image{imageIndex}.jpg");

                // Save the next image. The overload without ImageFormat saves the image in its original format.
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }
        } // PdfExtractor is disposed here

        // ---------- Generate simple HTML gallery ----------
        StringBuilder html = new StringBuilder();

        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head>");
        html.AppendLine("    <meta charset=\"UTF-8\">");
        html.AppendLine("    <title>Extracted Image Gallery</title>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");
        html.AppendLine("    <h1>Extracted Images</h1>");

        // Add each extracted image to the HTML
        foreach (string filePath in Directory.GetFiles(imagesFolder))
        {
            string fileName = Path.GetFileName(filePath);
            // Use relative path so the HTML can locate the images
            string relativePath = $"{imagesFolder}/{fileName}";
            html.AppendLine("    <div style=\"margin:10px; display:inline-block;\">");
            html.AppendLine($"        <img src=\"{relativePath}\" alt=\"{fileName}\" style=\"max-width:200px; height:auto;\"/>");
            html.AppendLine("    </div>");
        }

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML file
        File.WriteAllText(htmlFile, html.ToString());

        Console.WriteLine($"Image extraction complete. Gallery created at '{htmlFile}'.");
    }
}
