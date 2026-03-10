using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgPath  = "output.svg";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file into a PDF document.
            // HtmlLoadOptions is required for HTML input.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Prepare SVG save options. Passing a SaveOptions instance
                // forces the output format to SVG (otherwise .Save(string) would produce PDF).
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the PDF document as an SVG file.
                pdfDoc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: {svgPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML → PDF conversion relies on GDI+ and works only on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}