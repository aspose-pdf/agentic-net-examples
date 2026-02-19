using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (adjust as needed)
        const string htmlPath = "input.html";

        // Output SVG file path (the extension determines the format)
        const string svgPath = "output.svg";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions
            var loadOptions = new HtmlLoadOptions();
            Document pdfDoc = new Document(htmlPath, loadOptions);

            // Save the document as SVG. The .svg extension triggers SvgSaveOptions internally.
            pdfDoc.Save(svgPath);

            Console.WriteLine($"HTML successfully converted to SVG and saved at '{svgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}