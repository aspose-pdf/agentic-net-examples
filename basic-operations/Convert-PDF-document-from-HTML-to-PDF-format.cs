using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input HTML file path.
        string htmlFile = Path.Combine(dataDir, "HTML-to-PDF.html");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlFile}");
            return;
        }

        // Load the HTML document using HtmlLoadOptions.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Document constructor with load options creates a PDF representation of the HTML.
        using (Document pdfDocument = new Document(htmlFile, loadOptions))
        {
            // Save the document as PDF. No SaveOptions needed; the default format is PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfFile}'");
    }
}