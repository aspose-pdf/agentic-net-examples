using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlLoadOptions, SvgSaveOptions
using Aspose.Pdf.Facades;      // not needed here but kept for completeness

class Program
{
    static void Main()
    {
        // Input HTML file and output SVG file paths
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is optional but explicit.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Initialize SVG save options (default constructor is sufficient for most cases)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG. Passing SvgSaveOptions ensures non‑PDF output.
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgPath}'");
        }
        // HTML‑to‑SVG conversion relies on GDI+ and throws on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}