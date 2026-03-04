using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file path.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");

        // Desired output PDF file path.
        string pdfPath = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load options for converting HTML to PDF.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML file; the Document constructor performs the conversion.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are required for PDF output.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: {pdfPath}");
    }
}