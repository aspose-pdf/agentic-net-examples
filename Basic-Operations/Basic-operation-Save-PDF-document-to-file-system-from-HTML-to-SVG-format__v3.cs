using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtml = "input.html";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputHtml))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtml}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(inputHtml, loadOptions))
            {
                // Initialize SvgSaveOptions for SVG output
                SvgSaveOptions saveOptions = new SvgSaveOptions();

                // Save the document as SVG – explicit SaveOptions are required for non‑PDF formats
                doc.Save(outputSvg, saveOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: '{outputSvg}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+, which is Windows‑only
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}