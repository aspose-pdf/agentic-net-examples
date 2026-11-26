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
            // Load the HTML file and render all content onto a single PDF page
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                IsRenderToSinglePage = true
            };

            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as PDF (no SaveOptions needed for PDF output)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to single‑page PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            // GDI+ library missing on non‑Windows platforms
            Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}