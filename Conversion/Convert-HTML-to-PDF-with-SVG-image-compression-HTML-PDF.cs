using System;
using System.IO;
using Aspose.Pdf; // HtmlLoadOptions resides directly in the Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions can be used to control loading behavior
        // (e.g., base path, encoding). No special SVG‑compression option exists for loading,
        // but Aspose.Pdf automatically compresses SVG graphics when converting to PDF.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save as PDF. The conversion process will embed SVG graphics efficiently.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}