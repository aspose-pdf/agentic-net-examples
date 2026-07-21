using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades; // PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF file
        const string outputHtml = "report.html";       // generated HTML report

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // StringBuilder to compose the HTML content
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head><meta charset=\"UTF-8\"><title>PDF Images Report</title></head>");
        html.AppendLine("<body>");
        html.AppendLine("<h1>Extracted Images</h1>");

        // Use PdfExtractor (Facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare the extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream in its original format
                using (MemoryStream rawImgStream = new MemoryStream())
                {
                    // The overload without ImageFormat extracts the image in its native format
                    extractor.GetNextImage(rawImgStream);
                    rawImgStream.Position = 0;

                    // Convert the raw image to PNG (ensures a consistent MIME type for the HTML report)
                    using (Image img = Image.FromStream(rawImgStream))
                    using (MemoryStream pngStream = new MemoryStream())
                    {
                        img.Save(pngStream, ImageFormat.Png);
                        string base64 = Convert.ToBase64String(pngStream.ToArray());

                        // Embed the image using a data URI
                        html.AppendLine($"<div><p>Image {imageIndex}</p>");
                        html.AppendLine($"<img src=\"data:image/png;base64,{base64}\" alt=\"Image {imageIndex}\" />");
                        html.AppendLine("</div>");
                    }

                    imageIndex++;
                }
            }
        }

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(outputHtml, html.ToString(), Encoding.UTF8);
        Console.WriteLine($"HTML report generated: {outputHtml}");
    }
}
