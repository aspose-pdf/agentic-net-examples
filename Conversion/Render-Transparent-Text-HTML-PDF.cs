using System;
using System.IO;
using Aspose.Pdf;

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

        try
        {
            // Load the HTML document. HtmlLoadOptions is a class, not a namespace.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF. No SaveOptions are needed for PDF output.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        // HTML‑to‑PDF conversion relies on GDI+, which is Windows‑only.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}