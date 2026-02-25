using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file. HtmlLoadOptions relies on GDI+, so this may throw on non‑Windows platforms.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as SVG. SvgSaveOptions is defined in Aspose.Pdf namespace.
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML → SVG saved to '{svgPath}'.");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available (e.g., macOS/Linux). Inform the user and skip the conversion.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}