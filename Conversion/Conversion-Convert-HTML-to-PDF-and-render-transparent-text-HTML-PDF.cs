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
            // Load the HTML file. HtmlLoadOptions is required for HTML input.
            // Note: This operation depends on GDI+ and works only on Windows.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF.
                // Transparent text (e.g., CSS opacity) is rendered automatically during the conversion.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available (non‑Windows platforms)
            Console.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}