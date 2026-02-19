using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input SVG file path and output PDF file path
        string svgPath = "input.svg";
        string pdfPath = "output.pdf";

        // Verify that the SVG source file exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found at '{svgPath}'.");
            return;
        }

        try
        {
            // Load the SVG file into a PDF document using SvgLoadOptions.
            var loadOptions = new SvgLoadOptions();
            // Example of customizing the load options (optional):
            // loadOptions.ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine;

            Document pdfDocument = new Document(svgPath, loadOptions);

            // Save the document as a standard PDF file.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"SVG successfully converted to PDF at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}