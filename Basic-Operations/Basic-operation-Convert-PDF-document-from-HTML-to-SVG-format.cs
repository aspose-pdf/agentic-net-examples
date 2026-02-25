using System;
using System.IO;
using Aspose.Pdf;   // HtmlLoadOptions, SvgSaveOptions, Document

class Program
{
    static void Main()
    {
        const string htmlInput  = "input.html";
        const string svgOutput  = "output.svg";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlInput))
        {
            Console.Error.WriteLine($"Source file not found: {htmlInput}");
            return;
        }

        try
        {
            // LoadOptions for HTML input
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // SaveOptions for SVG output
            SvgSaveOptions saveOptions = new SvgSaveOptions();

            // Perform conversion: HTML -> SVG
            // Uses the static Document.Convert method (source file, load options, destination file, save options)
            Document.Convert(htmlInput, loadOptions, svgOutput, saveOptions);

            Console.WriteLine($"Conversion completed: '{svgOutput}'");
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