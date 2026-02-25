using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";
        const string logPath  = "conversion.log";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document (requires GDI+ on Windows)
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Convert the document to PDF/A (or PDF/X) format
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A/X document as SVG
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to PDF/A and saved as SVG: '{svgPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑PDF conversion uses GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}