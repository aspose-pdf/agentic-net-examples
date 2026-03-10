using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string htmlPath  = "metadata.html";      // output HTML file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the Facade class PdfFileInfo
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve required metadata properties
            string title   = info.Title   ?? string.Empty;
            string author  = info.Author  ?? string.Empty;
            string subject = info.Subject ?? string.Empty;
            string keywords = info.Keywords ?? string.Empty;

            // Build a simple HTML document displaying the metadata
            string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>PDF Metadata</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 20px; }}
        table {{ border-collapse: collapse; width: 50%; }}
        th, td {{ border: 1px solid #ccc; padding: 8px; text-align: left; }}
        th {{ background-color: #f2f2f2; }}
    </style>
</head>
<body>
    <h1>PDF Metadata</h1>
    <table>
        <tr><th>Title</th><td>{System.Web.HttpUtility.HtmlEncode(title)}</td></tr>
        <tr><th>Author</th><td>{System.Web.HttpUtility.HtmlEncode(author)}</td></tr>
        <tr><th>Subject</th><td>{System.Web.HttpUtility.HtmlEncode(subject)}</td></tr>
        <tr><th>Keywords</th><td>{System.Web.HttpUtility.HtmlEncode(keywords)}</td></tr>
    </table>
</body>
</html>";

            // Save the HTML content to a file
            File.WriteAllText(htmlPath, htmlContent);
            Console.WriteLine($"Metadata HTML saved to '{htmlPath}'.");
        }
    }
}