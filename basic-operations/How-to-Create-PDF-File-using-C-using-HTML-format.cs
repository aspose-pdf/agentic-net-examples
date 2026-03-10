using System;
using System.IO;
using Aspose.Pdf; // Core API for loading HTML and saving PDF

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file path.
        string htmlFile = Path.Combine(dataDir, "HTML-to-PDF.html");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Ensure the input file exists.
        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"Input HTML file not found: {htmlFile}");
            return;
        }

        // Load the HTML document with default load options.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document pdfDocument = new Document(htmlFile, loadOptions))
        {
            // Save the loaded content as a PDF file.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfFile}");
    }
}