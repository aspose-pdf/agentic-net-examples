using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file and output SVG file paths
        const string htmlPath = "input.html";
        const string svgPath = "output.svg";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML file into a PDF document
            var loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Prepare SVG save options (default settings are sufficient for most cases)
            var svgOptions = new SvgSaveOptions(); // SvgSaveOptions resides in Aspose.Pdf namespace

            // Save the document directly to SVG format
            pdfDocument.Save(svgPath, svgOptions);

            Console.WriteLine($"HTML successfully converted to SVG and saved at '{svgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}