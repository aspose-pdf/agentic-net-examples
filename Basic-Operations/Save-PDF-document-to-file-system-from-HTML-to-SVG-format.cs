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
            // Load the HTML file as a PDF document using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Initialize SVG save options (required for non‑PDF output)
                SvgSaveOptions saveOptions = new SvgSaveOptions();

                // Save the document as SVG; passing the options ensures correct format
                doc.Save(svgPath, saveOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: {svgPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}