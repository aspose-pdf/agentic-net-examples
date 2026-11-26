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

        try
        {
            // Load options: use the print media type for CSS @media print rules
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                HtmlMediaType = HtmlMediaType.Print
            };

            // Load the HTML document with the specified options and save as PDF
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires GDI+ and is only supported on Windows platforms.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}