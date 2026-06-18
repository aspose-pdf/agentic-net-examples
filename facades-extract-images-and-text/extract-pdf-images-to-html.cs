using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class ImageExtractorToHtml
{
    static void Main()
    {
        // Input PDF file containing images.
        const string pdfPath = "input.pdf";

        // Output HTML file that will embed the extracted images as Base64 data URIs.
        const string htmlPath = "report.html";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // StringBuilder to compose the HTML report.
        StringBuilder htmlBuilder = new StringBuilder();

        // Basic HTML header.
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("    <meta charset=\"utf-8\" />");
        htmlBuilder.AppendLine("    <title>Extracted Images Report</title>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Images extracted from PDF</h1>");

        // Use PdfExtractor (a Facade) to extract images from the PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to work with images.
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images.
            while (extractor.HasNextImage())
            {
                // Store the current image in a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);

                    // Convert the image bytes to a Base64 string.
                    string base64 = Convert.ToBase64String(imageStream.ToArray());

                    // PdfExtractor outputs JPEG by default; adjust MIME type if needed.
                    const string mimeType = "image/jpeg";

                    // Append an <img> tag with the Base64 data URI to the HTML.
                    htmlBuilder.AppendLine($"    <div>");
                    htmlBuilder.AppendLine($"        <p>Image {imageIndex}</p>");
                    htmlBuilder.AppendLine($"        <img src=\"data:{mimeType};base64,{base64}\" alt=\"Extracted Image {imageIndex}\" />");
                    htmlBuilder.AppendLine($"    </div>");
                    htmlBuilder.AppendLine("<hr/>");

                    imageIndex++;
                }
            }
        }

        // Close HTML tags.
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the generated HTML to the output file.
        File.WriteAllText(htmlPath, htmlBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"HTML report with embedded images saved to '{htmlPath}'.");
    }
}