using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HTML save options and enable SVG compression
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                CompressSvgGraphicsIfAny = true
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with compressed SVG assets: {outputHtmlPath}");
    }
}