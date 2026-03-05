using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";   // source HTML file
        const string pdfPath  = "output.pdf";   // destination PDF file

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is required for non‑PDF sources.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save as PDF. No SaveOptions are needed because PDF is the native output format.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML loading uses GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}