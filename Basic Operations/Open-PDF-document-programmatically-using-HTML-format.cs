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
            Console.Error.WriteLine($"File not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Document implements IDisposable – use a using block for deterministic cleanup
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}