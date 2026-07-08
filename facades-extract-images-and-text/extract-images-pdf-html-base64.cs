using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades; // PdfExtractor

class Program
{
    static void Main()
    {
        // Input PDF file containing images
        const string pdfPath = "input.pdf";

        // Output HTML file that will embed the extracted images as Base64 data URIs
        const string htmlPath = "report.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // StringBuilder to compose the HTML report
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Extracted Images</title></head>");
        htmlBuilder.AppendLine("<body>");

        // Use PdfExtractor (Facade) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream (original format)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Get the next image. The overload without ImageFormat returns the image in its original format.
                    extractor.GetNextImage(imageStream);

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());

                    // Determine MIME type from the first few bytes (optional). For simplicity, assume PNG.
                    // If you need accurate detection, inspect the header bytes.
                    string mimeType = "image/png";

                    // Embed the image in the HTML using a data URI
                    htmlBuilder.AppendLine(
                        $"<div><p>Image {imageIndex}:</p>" +
                        $"<img src=\"data:{mimeType};base64,{base64}\" alt=\"Image {imageIndex}\"/></div>");

                    imageIndex++;
                }
            }
        }

        // Close the HTML document
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(htmlPath, htmlBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"HTML report with embedded images saved to '{htmlPath}'.");
    }
}
