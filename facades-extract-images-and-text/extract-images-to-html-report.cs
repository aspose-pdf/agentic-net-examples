using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // Added for ImageFormat

class Program
{
    static void Main()
    {
        // Paths for input PDF and output HTML report
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "report.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // List to hold generated <img> tags
        List<string> imageTags = new List<string>();

        // Use PdfExtractor (Facade) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file – this is the required load operation
            extractor.BindPdf(inputPdfPath);

            // Extract images from the bound document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream as PNG
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());
                    // Build a data‑URI <img> tag
                    string imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Image {imageIndex}\" />";
                    imageTags.Add(imgTag);
                }
                imageIndex++;
            }
        }

        // Assemble a simple HTML document containing all images
        string htmlContent = "<!DOCTYPE html>\n" +
                             "<html>\n" +
                             "<head>\n" +
                             "    <meta charset=\"UTF-8\">\n" +
                             "    <title>PDF Image Report</title>\n" +
                             "    <style>\n" +
                             "        body { font-family: Arial, sans-serif; margin: 20px; }\n" +
                             "        img { display: block; margin-bottom: 20px; max-width: 100%; height: auto; }\n" +
                             "    </style>\n" +
                             "</head>\n" +
                             "<body>\n" +
                             "    <h1>Extracted Images</h1>\n" +
                             string.Join(Environment.NewLine, imageTags) + "\n" +
                             "</body>\n" +
                             "</html>";

        // Save the HTML report – plain file write (no Aspose PDF SaveOptions needed)
        File.WriteAllText(outputHtmlPath, htmlContent);
        Console.WriteLine($"HTML report generated: {outputHtmlPath}");
    }
}
