using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath  = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Verify that the HTML source exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document using HtmlLoadOptions.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the loaded document as PDF. No SaveOptions are required for PDF output.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}