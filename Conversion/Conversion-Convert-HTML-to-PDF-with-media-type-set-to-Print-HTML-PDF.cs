using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, HtmlMediaType)

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify input file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Configure HTML loading options and set the media type to Print
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            loadOptions.HtmlMediaType = HtmlMediaType.Print; // media type influences rendering (Print layout)

            // Load the HTML file into a PDF Document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Save without explicit SaveOptions – Document.Save(string) writes PDF regardless of extension
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        // HTML‑to‑PDF conversion relies on GDI+ and is Windows‑only; handle cross‑platform scenarios gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. HTML to PDF conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}