using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Directory that contains the source HTML file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file path.
        string htmlPath = Path.Combine(dataDir, "input.html");

        // Desired output PDF file path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the HTML source exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load options for converting HTML to PDF.
        // Using the parameterless constructor creates default options (empty base path).
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML document into an Aspose.Pdf Document instance.
        // The constructor overload (string, LoadOptions) is the correct way to load HTML.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the loaded document as PDF.
            // No SaveOptions are required because the target format is PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}