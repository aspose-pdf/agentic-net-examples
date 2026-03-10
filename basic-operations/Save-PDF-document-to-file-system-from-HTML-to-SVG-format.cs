using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, HtmlLoadOptions, SvgSaveOptions)

class Program
{
    static void Main()
    {
        // Input HTML file and output SVG file paths
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";

        // Verify the source HTML exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document – HtmlLoadOptions tells Aspose.Pdf to treat the source as HTML
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Initialize SVG save options (default constructor is sufficient for most scenarios)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG. The overload with SaveOptions is required
                // because Document.Save(string) without options always writes PDF.
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: {svgPath}");
        }
        // HTML‑to‑SVG conversion relies on GDI+ and throws TypeInitializationException on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}