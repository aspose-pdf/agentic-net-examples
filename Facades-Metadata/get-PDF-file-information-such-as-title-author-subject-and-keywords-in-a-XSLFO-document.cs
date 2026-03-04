using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string foPath  = "output.fo";      // XSL‑FO output

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfFileInfo facade reads document metadata
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve required fields (null‑coalesced to empty strings)
            string title    = info.Title    ?? string.Empty;
            string author   = info.Author   ?? string.Empty;
            string subject  = info.Subject  ?? string.Empty;
            string keywords = info.Keywords ?? string.Empty;

            // Build a minimal XSL‑FO document containing the metadata
            string xslFo = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<fo:root xmlns:fo=""http://www.w3.org/1999/XSL/Format"">
  <fo:layout-master-set>
    <fo:simple-page-master master-name=""A4"" page-height=""29.7cm"" page-width=""21cm"" margin=""2cm"">
      <fo:region-body/>
    </fo:simple-page-master>
  </fo:layout-master-set>
  <fo:page-sequence master-reference=""A4"">
    <fo:flow flow-name=""xsl-region-body"">
      <fo:block font-size=""14pt"" font-weight=""bold"">PDF Metadata</fo:block>
      <fo:block>Title: {EscapeXml(title)}</fo:block>
      <fo:block>Author: {EscapeXml(author)}</fo:block>
      <fo:block>Subject: {EscapeXml(subject)}</fo:block>
      <fo:block>Keywords: {EscapeXml(keywords)}</fo:block>
    </fo:flow>
  </fo:page-sequence>
</fo:root>";

            // Write the XSL‑FO to a file
            File.WriteAllText(foPath, xslFo);
            Console.WriteLine($"XSL‑FO saved to '{foPath}'.");
        }
    }

    // Simple XML escaping to avoid malformed XSL‑FO
    static string EscapeXml(string text)
    {
        return string.IsNullOrEmpty(text) ? string.Empty : System.Security.SecurityElement.Escape(text);
    }
}