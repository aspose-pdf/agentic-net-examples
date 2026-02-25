using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";
        const string logPath  = "conversion_log.xml";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document (requires GDI+ on Windows)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Convert the intermediate PDF to PDF/A (or PDF/X) format
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the result as SVG using explicit save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML → PDF/A → SVG saved to '{svgPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion is Windows‑only (GDI+)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}