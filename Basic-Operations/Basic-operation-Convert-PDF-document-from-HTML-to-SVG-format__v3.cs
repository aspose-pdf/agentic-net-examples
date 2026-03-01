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
            // Load the HTML file using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the document as SVG using SvgSaveOptions
                SvgSaveOptions saveOptions = new SvgSaveOptions();
                doc.Save(svgPath, saveOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML to SVG conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}