using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and HtmlLoadOptions are in this namespace

class HtmlToPdfConverter
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file. HtmlLoadOptions can be customized if required.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Document constructor loads the HTML using the provided options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}