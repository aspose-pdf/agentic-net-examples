using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions and LoadOptions are in this namespace

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";

        // Verify the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is optional but shown for clarity.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as SVG. Passing SvgSaveOptions ensures non‑PDF output.
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgPath}'.");
        }
        // HTML‑to‑PDF conversion relies on GDI+, which is Windows‑only.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}