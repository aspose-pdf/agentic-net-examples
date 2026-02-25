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
            // Load the HTML file into a Document (requires GDI+ on Windows)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as SVG using explicit SvgSaveOptions
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgPath}'");
        }
        catch (TypeInitializationException)
        {
            // GDI+ is unavailable on non‑Windows platforms
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}