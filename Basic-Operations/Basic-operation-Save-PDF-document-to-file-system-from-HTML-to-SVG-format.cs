using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input HTML and output SVG
        const string htmlPath = "input.html";
        const string svgPath = "output.svg";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        // Load the HTML file into a PDF document using HtmlLoadOptions
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        Document pdfDocument = new Document(htmlPath, loadOptions);

        // Save the PDF document as SVG.
        // The .Save(string) overload infers the format from the file extension.
        pdfDocument.Save(svgPath);

        Console.WriteLine($"Conversion completed. SVG saved to '{svgPath}'.");
    }
}