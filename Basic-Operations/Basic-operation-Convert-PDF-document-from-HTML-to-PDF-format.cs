using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions.
            // HtmlLoadOptions resides in the Aspose.Pdf namespace.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // The Document constructor that accepts a file name and load options
            // creates a PDF document from the HTML source.
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save the resulting PDF. No SaveOptions are needed because PDF is the default format.
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        // HTML-to-PDF conversion relies on GDI+; on non‑Windows platforms this may throw.
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}