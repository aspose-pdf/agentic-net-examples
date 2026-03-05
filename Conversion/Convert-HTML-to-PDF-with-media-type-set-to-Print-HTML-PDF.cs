using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure load options: set media type to Print
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            HtmlMediaType = HtmlMediaType.Print
        };

        // Load the HTML document into a PDF Document instance
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML converted to PDF (Print media) saved at '{pdfPath}'.");
    }
}