using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string htmlPath  = "input.html";   // source HTML file
        const string pdfPath   = "output.pdf";   // destination PDF file

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // HtmlLoadOptions are optional; using the default constructor ensures
            // the HTML is loaded with default settings (base path empty).
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML document inside a using block for deterministic disposal.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF. No SaveOptions are required because the target format is PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML-to-PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML-to-PDF conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}