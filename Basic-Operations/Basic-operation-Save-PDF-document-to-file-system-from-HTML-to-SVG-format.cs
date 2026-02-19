using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main(string[] args)
    {
        // Paths can be passed as arguments or hard‑coded for a quick test.
        string htmlPath = args.Length > 0 ? args[0] : "input.html";
        string svgPath  = args.Length > 1 ? args[1] : "output.svg";

        // Verify that the source HTML file exists before attempting conversion.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions enables proper parsing of HTML content.
            Document pdfDocument = new Document(htmlPath, new HtmlLoadOptions());

            // Save the document as SVG. The file extension determines the output format.
            pdfDocument.Save(svgPath);

            Console.WriteLine($"Conversion successful. SVG saved to '{svgPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., parsing issues, I/O problems) and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}