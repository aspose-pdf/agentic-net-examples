using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, HtmlLoadOptions, etc.

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

        try
        {
            // Load the HTML file using default HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as PDF (default format, no SaveOptions needed)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        // HTML‑to‑PDF conversion relies on GDI+ and may fail on non‑Windows platforms
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