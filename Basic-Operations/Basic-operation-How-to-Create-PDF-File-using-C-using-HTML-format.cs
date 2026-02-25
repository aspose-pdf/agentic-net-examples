using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Path to the HTML file to be converted.
        const string htmlPath = "input.html";

        // Desired output PDF file path.
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions can be omitted if default settings are sufficient.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the loaded document as PDF. No SaveOptions are required for PDF output.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            // Generic error handling.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}