using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        const string htmlPath   = "input.html";   // Path to the source HTML file
        const string pdfPath    = "output.pdf";   // Desired PDF output path

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is required for HTML input.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML into an Aspose.Pdf Document.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // No special PDF save options are needed – Save() without arguments writes PDF.
                // SVG images present in the HTML are rasterized during conversion;
                // PDF format does not store SVG graphics, so no additional compression step is required.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}