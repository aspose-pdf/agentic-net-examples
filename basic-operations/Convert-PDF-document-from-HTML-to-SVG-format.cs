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
            // Convert HTML to SVG using the static Convert method.
            // HtmlLoadOptions tells Aspose.Pdf how to read the HTML source.
            // SvgSaveOptions specifies that the output format is SVG.
            Document.Convert(htmlPath, new HtmlLoadOptions(), svgPath, new SvgSaveOptions());

            Console.WriteLine($"Conversion succeeded: '{svgPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+, which is only available on Windows.
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}