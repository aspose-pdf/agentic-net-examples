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
            // This operation uses GDI+ and is Windows‑only, so we catch platform‑specific exceptions.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the loaded document as PDF. No SaveOptions needed because the target format is PDF.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // Thrown on non‑Windows platforms because HTML conversion depends on GDI+.
            Console.WriteLine("HTML‑to‑PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library missing.
            Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}