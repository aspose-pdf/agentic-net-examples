using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document, convert to HTML using default HtmlSaveOptions, and save.
        // Document is wrapped in a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize default HTML save options (no custom settings required)
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Save the document as HTML; passing HtmlSaveOptions ensures non‑PDF output.
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
    }
}