using System;
using System.IO;
using Aspose.Pdf; // HtmlLoadOptions, SvgSaveOptions, Document

class Program
{
    static void Main()
    {
        const string htmlInputPath  = "input.html";
        const string svgOutputPath = "output.svg";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML file as a PDF document using HtmlLoadOptions.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document pdfDoc = new Document(htmlInputPath, loadOptions))
            {
                // Save the document as SVG using explicit SvgSaveOptions.
                SvgSaveOptions saveOptions = new SvgSaveOptions();
                pdfDoc.Save(svgOutputPath, saveOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{svgOutputPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}