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
            // Load the HTML file using HtmlLoadOptions.
            using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the document as PDF (default format, no SaveOptions needed).
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"PDF created successfully at '{pdfPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion relies on GDI+ and works only on Windows.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}