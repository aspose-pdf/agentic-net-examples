using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source HTML file and the destination PDF file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the HTML file exists before attempting conversion.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions can be customized if needed;
        // here we use the default constructor (empty base path).
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the loaded content as a PDF file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}