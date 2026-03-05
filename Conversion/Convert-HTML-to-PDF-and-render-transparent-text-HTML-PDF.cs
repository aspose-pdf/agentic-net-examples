using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and HtmlLoadOptions are in this namespace

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions uses default settings,
        // which preserve visual properties such as opacity (transparent text).
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Convert HTML to PDF inside a using block to ensure deterministic disposal.
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Save the result as PDF. No additional options are required;
            // transparent text is rendered automatically during the conversion.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}
