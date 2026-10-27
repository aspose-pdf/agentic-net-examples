using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputHtmlPath = "signature_report.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to compose the HTML report
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html lang=\"en\">");
        htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Signature Images Report</title></head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Extracted Signature Images</h1>");

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // XImageCollection is not a dictionary; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Determine MIME type based on image format (fallback to png)
                        string mimeType = "image/png";

                        // Encode image as Base64 for embedding in HTML
                        string base64 = Convert.ToBase64String(imageBytes);
                        string dataUrl = $"data:{mimeType};base64,{base64}";

                        // Append image to HTML
                        htmlBuilder.AppendLine($"<div style=\"margin-bottom:20px;\">");
                        htmlBuilder.AppendLine($"<p>Image {imageCounter} (Page {pageIndex})</p>");
                        htmlBuilder.AppendLine($"<img src=\"{dataUrl}\" alt=\"Signature Image {imageCounter}\" style=\"max-width:100%; height:auto;\"/>");
                        htmlBuilder.AppendLine($"</div>");

                        imageCounter++;
                    }
                }
            }
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML report to disk
        try
        {
            File.WriteAllText(outputHtmlPath, htmlBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Signature report generated: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write HTML report: {ex.Message}");
        }
    }
}