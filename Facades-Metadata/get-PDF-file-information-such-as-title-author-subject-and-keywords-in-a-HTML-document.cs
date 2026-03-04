using System;
using System.IO;
using System.Net;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "info.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve required properties (null‑coalesced to empty strings)
            string title    = pdfInfo.Title    ?? string.Empty;
            string author   = pdfInfo.Author   ?? string.Empty;
            string subject  = pdfInfo.Subject  ?? string.Empty;
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Build a simple HTML document with the metadata
            string html = $"<html><head><title>PDF Information</title></head><body>" +
                          $"<h1>PDF Metadata</h1>" +
                          $"<p><strong>Title:</strong> {WebUtility.HtmlEncode(title)}</p>" +
                          $"<p><strong>Author:</strong> {WebUtility.HtmlEncode(author)}</p>" +
                          $"<p><strong>Subject:</strong> {WebUtility.HtmlEncode(subject)}</p>" +
                          $"<p><strong>Keywords:</strong> {WebUtility.HtmlEncode(keywords)}</p>" +
                          $"</body></html>";

            // Save the HTML content to a file
            File.WriteAllText(htmlPath, html);
        }

        Console.WriteLine($"Metadata extracted to '{htmlPath}'.");
    }
}