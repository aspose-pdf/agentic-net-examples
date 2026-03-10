using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string svgPath = "output.svg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve required properties
            string title   = info.Title   ?? string.Empty;
            string author  = info.Author  ?? string.Empty;
            string subject = info.Subject ?? string.Empty;
            string keywords = info.Keywords ?? string.Empty;

            // Build a simple SVG document embedding the metadata
            string svgContent = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
  <metadata>
    <dc:title>{EscapeXml(title)}</dc:title>
    <dc:creator>{EscapeXml(author)}</dc:creator>
    <dc:description>{EscapeXml(subject)}</dc:description>
    <dc:keywords>{EscapeXml(keywords)}</dc:keywords>
  </metadata>
  <!-- Example graphic -->
  <rect x=""10"" y=""10"" width=""200"" height=""100"" fill=""lightgray"" stroke=""black""/>
</svg>";

            // Save the SVG content to file
            File.WriteAllText(svgPath, svgContent);
            Console.WriteLine($"SVG with embedded metadata saved to '{svgPath}'.");
        }
    }

    // Simple XML escaping to avoid invalid characters in the SVG metadata
    private static string EscapeXml(string value)
    {
        return System.Security.SecurityElement.Escape(value);
    }
}