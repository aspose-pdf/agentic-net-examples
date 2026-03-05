using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        // Configure load options for HTML → PDF conversion.
        // Setting IsEmbedFonts to false disables embedding of fonts into the resulting PDF.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsEmbedFonts = false   // Do NOT embed fonts.
        };

        // Load the HTML document using the specified options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF. No SaveOptions are required for PDF output.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF without embedded fonts: {pdfPath}");
    }
}