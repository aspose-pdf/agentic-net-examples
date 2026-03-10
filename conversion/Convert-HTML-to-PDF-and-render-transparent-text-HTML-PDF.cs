using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and HtmlLoadOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";

        // Desired output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the source HTML exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document using HtmlLoadOptions (no custom options needed for transparency)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Create a Document from the HTML source
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF. No additional SaveOptions are required.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}