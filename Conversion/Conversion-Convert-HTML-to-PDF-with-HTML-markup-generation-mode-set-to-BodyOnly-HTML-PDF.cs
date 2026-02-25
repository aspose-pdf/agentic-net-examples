using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file into a Document using HtmlLoadOptions.
        // HtmlLoadOptions provides control over HTML parsing; no markup‑generation mode is needed here.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal (lifecycle rule).
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // When saving to PDF, no SaveOptions are required.
            // The HtmlMarkupGenerationMode property belongs to HtmlSaveOptions (used when saving PDF → HTML),
            // so it is not applicable for HTML → PDF conversion.
            doc.Save(pdfPath); // Saves the document as PDF (extension determines format).
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}