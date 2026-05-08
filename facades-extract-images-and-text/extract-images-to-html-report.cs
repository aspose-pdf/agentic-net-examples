using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "report.html";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to compose the HTML report
        StringBuilder htmlBuilder = new StringBuilder();

        // Basic HTML skeleton
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
        htmlBuilder.AppendLine("    <title>Extracted Images Report</title>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("    <h1>Images extracted from PDF</h1>");

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream); // default format is JPEG
                    byte[] imageBytes = imageStream.ToArray();

                    // Convert image bytes to a Base64 string
                    string base64Image = Convert.ToBase64String(imageBytes);

                    // Embed the image using a data URI (assumed JPEG)
                    htmlBuilder.AppendLine("    <div style=\"margin-bottom:20px;\">");
                    htmlBuilder.AppendLine($"        <p>Image {imageIndex}</p>");
                    htmlBuilder.AppendLine($"        <img src=\"data:image/jpeg;base64,{base64Image}\" alt=\"Image {imageIndex}\"/>");
                    htmlBuilder.AppendLine("    </div>");
                }

                imageIndex++;
            }
        }

        // Close HTML tags
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML report to disk
        File.WriteAllText(outputHtmlPath, htmlBuilder.ToString());

        Console.WriteLine($"HTML report with embedded images saved to '{outputHtmlPath}'.");
    }
}