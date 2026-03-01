using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Path to the directory containing the input HTML file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath  = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Ensure the input file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Input HTML file not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions is a class, not a namespace; instantiate it directly.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML document using the load options and save it as PDF.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}