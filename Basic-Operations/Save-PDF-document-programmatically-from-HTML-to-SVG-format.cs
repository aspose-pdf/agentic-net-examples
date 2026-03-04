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
            // Load the HTML file as a PDF document.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // Configure SVG save options.
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document to SVG format.
                pdfDoc.Save(svgPath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion uses GDI+, which is only available on Windows.
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}