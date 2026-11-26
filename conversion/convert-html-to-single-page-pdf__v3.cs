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
            // Load the HTML file with the option to render all content on a single page
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                IsRenderToSinglePage = true
            };

            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save as PDF (default format)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to single‑page PDF: {pdfPath}");
        }
        // HTML‑to‑PDF conversion requires GDI+ (Windows only)
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. HTML to PDF conversion is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}