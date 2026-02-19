using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument) and output SVG file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputHtmlPath> <outputSvgPath>");
            return;
        }

        string htmlPath = args[0];
        string svgPath = args[1];

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document into an Aspose.Pdf Document object
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the document as SVG. The simple Save method infers the format from the file extension.
            pdfDocument.Save(svgPath);

            Console.WriteLine($"Conversion successful. SVG saved to '{svgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
