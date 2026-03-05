using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and desired PDF output file
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions is used to control HTML loading;
        // no special options are required for SVG compression during HTML‑to‑PDF conversion.
        var loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the loaded document as PDF. No SaveOptions are needed because
            // the default format for Save(string) is PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}