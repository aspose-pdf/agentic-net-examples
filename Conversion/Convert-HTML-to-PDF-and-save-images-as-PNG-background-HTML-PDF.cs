using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file. Images (including PNG) are imported as they appear in the HTML.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF. Embedded images retain their original format (PNG will stay PNG).
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}