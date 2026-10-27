using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            var htmlBuilder = new StringBuilder();

            // Basic HTML skeleton
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Extracted Images</title></head><body>");
            htmlBuilder.AppendLine("<h1>Images extracted from PDF</h1>");

            int imageIndex = 0;

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Save the image to a memory stream in its original format
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Determine MIME type by inspecting the image header
                        string mime = GetMimeType(imageBytes);

                        htmlBuilder.AppendLine("<div>");
                        htmlBuilder.AppendLine($"<p>Page {pageNum}, Image {++imageIndex}</p>");
                        string base64 = Convert.ToBase64String(imageBytes);
                        htmlBuilder.AppendLine($"<img src=\"data:{mime};base64,{base64}\" alt=\"Extracted image\"/>");
                        htmlBuilder.AppendLine("</div>");
                    }
                }
            }

            htmlBuilder.AppendLine("</body></html>");

            // Write the generated HTML to disk
            File.WriteAllText(outputHtml, htmlBuilder.ToString());
            Console.WriteLine($"HTML report created at '{outputHtml}'.");
        }
    }

    /// <summary>
    /// Very small header‑based MIME detection. Supports JPEG, PNG, GIF, BMP. Falls back to PNG.
    /// </summary>
    private static string GetMimeType(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 4)
            return "image/png"; // safe default

        // JPEG: FF D8 FF
        if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
            return "image/jpeg";
        // PNG: 89 50 4E 47
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return "image/png";
        // GIF: 47 49 46 38
        if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x38)
            return "image/gif";
        // BMP: 42 4D
        if (bytes[0] == 0x42 && bytes[1] == 0x4D)
            return "image/bmp";

        // Default fallback
        return "image/png";
    }
}
