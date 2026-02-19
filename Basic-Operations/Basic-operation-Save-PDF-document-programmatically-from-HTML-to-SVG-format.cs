using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (can be passed as first argument)
        string htmlPath = args.Length > 0 ? args[0] : "input.html";
        // Output SVG file path (can be passed as second argument)
        string svgPath = args.Length > 1 ? args[1] : "output.svg";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document into an Aspose.Pdf Document
            Document pdfDocument = new Document(htmlPath, new HtmlLoadOptions());

            // Save the document as SVG; the format is inferred from the .svg extension
            pdfDocument.Save(svgPath);

            Console.WriteLine($"HTML successfully converted to SVG and saved at '{svgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}