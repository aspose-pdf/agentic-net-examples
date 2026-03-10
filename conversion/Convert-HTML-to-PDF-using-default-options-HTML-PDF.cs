using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath   = "input.html";
        const string pdfPath    = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file using HtmlLoadOptions (default constructor)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF using default options
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}