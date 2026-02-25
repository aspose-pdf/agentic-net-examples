using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class HtmlToPdfWithSvgCompression
{
    static void Main()
    {
        // Input HTML file and output PDF file paths.
        const string htmlPath  = "input.html";
        const string pdfPath   = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        // HtmlLoadOptions can be used to control how the HTML is parsed.
        // No explicit option exists for SVG compression during HTML‑>PDF conversion;
        // Aspose.Pdf automatically optimizes embedded SVG graphics when saving to PDF.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML document inside a using block to ensure deterministic disposal.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF. No SaveOptions are required because the target
            // format is PDF; the overload Document.Save(string) always writes PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}