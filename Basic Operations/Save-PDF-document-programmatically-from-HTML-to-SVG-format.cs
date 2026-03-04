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
            // Load the HTML file into a PDF document using HtmlLoadOptions
            using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Initialize SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the PDF document as an SVG file (non‑PDF format requires explicit options)
                pdfDoc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: {svgPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}