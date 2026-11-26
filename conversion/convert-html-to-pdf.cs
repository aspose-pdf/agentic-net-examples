using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file using default options. Recent Aspose.Pdf versions no longer expose HtmlMediaType/HtmlMediaTypes.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Saving without SaveOptions writes PDF (the default format)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        // HTML‑to‑PDF conversion relies on GDI+, which is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
