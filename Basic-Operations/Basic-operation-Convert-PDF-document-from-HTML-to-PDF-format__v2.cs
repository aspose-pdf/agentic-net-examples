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

        // Load the HTML file. HtmlLoadOptions can be customized if needed.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Saving without a SaveOptions argument writes PDF regardless of the extension.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Successfully converted HTML to PDF: '{pdfPath}'");
    }
}