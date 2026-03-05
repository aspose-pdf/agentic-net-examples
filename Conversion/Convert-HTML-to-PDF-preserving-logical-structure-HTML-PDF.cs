using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

// Convert an HTML file to a PDF while preserving the logical (tagged) structure.
class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        // Load the HTML document using HtmlLoadOptions.
        // HtmlLoadOptions enables the HTML‑to‑PDF conversion engine.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // The Document constructor (string, HtmlLoadOptions) loads the HTML.
        // The using block ensures deterministic disposal of the Document.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF.
            // The Save(string) overload writes a PDF regardless of the file extension.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF with logical structure: {pdfPath}");
    }
}
