using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string htmlPath = "source.html";
        const string tempPdfPath = "temp.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // ---------- Extract metadata from HTML ----------
        string htmlContent = File.ReadAllText(htmlPath);

        // Simple regexes for <title> and <meta name="..."> tags
        string title = ExtractTagContent(htmlContent, @"<title>(.*?)</title>", RegexOptions.IgnoreCase);
        string author = ExtractMetaContent(htmlContent, "author");
        string subject = ExtractMetaContent(htmlContent, "description"); // using description as subject
        string keywords = ExtractMetaContent(htmlContent, "keywords");

        // ---------- Convert HTML to PDF ----------
        using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
        {
            // Save intermediate PDF; metadata will be added later via PdfFileInfo
            htmlDoc.Save(tempPdfPath);
        }

        // ---------- Apply metadata using PdfFileInfo (Facades API) ----------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
        {
            // Assign extracted metadata
            pdfInfo.Title = title;
            pdfInfo.Author = author;
            pdfInfo.Subject = subject;
            pdfInfo.Keywords = keywords;

            // Save the final PDF with updated metadata
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        // Clean up temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore */ }

        Console.WriteLine($"PDF created with metadata: {outputPdfPath}");
    }

    // Helper to extract content between tags
    private static string ExtractTagContent(string source, string pattern, RegexOptions options)
    {
        var match = Regex.Match(source, pattern, options);
        return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
    }

    // Helper to extract <meta name="..."> content
    private static string ExtractMetaContent(string source, string metaName)
    {
        string pattern = $@"<meta\s+[^>]*name\s*=\s*[""']{metaName}[""'][^>]*content\s*=\s*[""']([^""']+)[""'][^>]*>";
        var match = Regex.Match(source, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
    }
}