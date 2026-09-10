using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HtmlSaveOptions and enable SVG compression.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
            htmlOptions.CompressSvgGraphicsIfAny = true; // compress SVG graphics into SVGZ

            // Save the document as HTML using the configured options.
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with compressed SVGs: '{outputHtmlPath}'");
    }
}