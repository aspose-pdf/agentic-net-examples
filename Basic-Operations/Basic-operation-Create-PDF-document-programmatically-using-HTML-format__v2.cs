using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file into a Document. HtmlLoadOptions is optional but shown for clarity.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the loaded content as a PDF file.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'.");
    }
}