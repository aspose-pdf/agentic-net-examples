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
            // Load the HTML file and convert it to a PDF document in memory
            using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the PDF document as SVG using explicit save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pdfDoc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"SVG file saved to '{svgPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}