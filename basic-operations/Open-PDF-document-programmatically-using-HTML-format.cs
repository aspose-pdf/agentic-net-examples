using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file (to be loaded as a PDF document)
        const string htmlPath = "input.html";

        // Output PDF file (result of the conversion)
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"File not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file using HtmlLoadOptions.
            // HtmlLoadOptions resides in the Aspose.Pdf namespace.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        // HTML‑to‑PDF conversion relies on GDI+ and throws TypeInitializationException on non‑Windows platforms.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}