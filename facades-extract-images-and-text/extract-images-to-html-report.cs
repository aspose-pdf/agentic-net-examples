using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string htmlPath  = "report.html";    // output HTML report

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // StringBuilder to compose the HTML report
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head><meta charset=\"UTF-8\"><title>Image Report</title></head>");
        html.AppendLine("<body>");
        html.AppendLine("<h1>Extracted Images</h1>");

        // Use PdfExtractor (Facade) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // load the PDF
            extractor.ExtractImage();            // start image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);   // default format is JPEG
                    byte[] imgBytes = imgStream.ToArray();

                    // Convert image bytes to Base64 string
                    string base64 = Convert.ToBase64String(imgBytes);

                    // Embed the image using a data URI
                    html.AppendLine($"<div><p>Image {imageIndex}</p>");
                    html.AppendLine($"<img src=\"data:image/jpeg;base64,{base64}\" alt=\"Image {imageIndex}\" />");
                    html.AppendLine("</div>");

                    imageIndex++;
                }
            }
        }

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(htmlPath, html.ToString(), Encoding.UTF8);
        Console.WriteLine($"HTML report generated at '{htmlPath}'.");
    }
}