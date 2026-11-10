using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "report.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Facade for extracting resources from a PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // NOTE: The ExtractImageMode property does not exist in the current Aspose.Pdf.Facades version.
            // Image extraction is performed by calling ExtractImage() which extracts all images by default.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility

            // Extract all images from the document
            extractor.ExtractImage();

            // Build an HTML report that embeds each image as a base64 data URI
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");
            htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Image Report</title></head>");
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<h1>Extracted Images</h1>");

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Use a temporary file because the current Aspose.Pdf API requires a file path
                string tempFile = Path.GetTempFileName();
                try
                {
                    // Extract the next image to the temporary file as PNG (you can change the format if needed)
                    extractor.GetNextImage(tempFile, ImageFormat.Png);

                    // Read the image bytes
                    byte[] imageBytes = File.ReadAllBytes(tempFile);

                    // Convert the image bytes to a Base64 string
                    string base64 = Convert.ToBase64String(imageBytes);

                    // The format we saved the image in (png in this case)
                    string format = "png";

                    // Append an <img> tag with a data URI to the HTML
                    htmlBuilder.AppendLine($"<div style=\"margin-bottom:20px;\">");
                    htmlBuilder.AppendLine($"  <p>Image {imageIndex} ({format.ToUpperInvariant()})</p>");
                    htmlBuilder.AppendLine($"  <img src=\"data:image/{format};base64,{base64}\" alt=\"Extracted Image {imageIndex}\" />");
                    htmlBuilder.AppendLine($"</div>");

                    imageIndex++;
                }
                finally
                {
                    // Clean up the temporary file
                    if (File.Exists(tempFile))
                        File.Delete(tempFile);
                }
            }

            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Write the HTML report to disk
            File.WriteAllText(htmlPath, htmlBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"HTML report generated: {htmlPath}");
        }
    }
}
