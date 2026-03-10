using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";      // source HTML file
        const string svgPath  = "output.svg";      // target SVG file
        const string logPath  = "conversion.log"; // log for PDF/A conversion

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load HTML document (requires GDI+ on Windows)
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Convert to PDF/A (or PDF/X) format
                // PDF/A‑1B is used here; change to PdfFormat.PDF_X_3 for PDF/X‑3
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A document as SVG
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to PDF/A and saved as SVG: {svgPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}