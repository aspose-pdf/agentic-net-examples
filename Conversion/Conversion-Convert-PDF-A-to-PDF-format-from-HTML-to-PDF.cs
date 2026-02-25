using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string htmlInputPath  = "input.html";   // Source HTML file (PDF/A source is not needed for this conversion)
        const string pdfOutputPath  = "output.pdf";   // Destination PDF file

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is required for HTML → PDF conversion.
            // This operation uses GDI+ and is Windows‑only; wrap it to handle possible TypeInitializationException.
            using (Document doc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Save as PDF. No SaveOptions are needed because the default format is PDF.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfOutputPath}'");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available (e.g., on macOS/Linux). Inform the user.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            // Generic error handling.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}