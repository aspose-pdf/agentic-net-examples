using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure loading options to render the entire HTML document onto a single PDF page.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsRenderToSinglePage = true
        };

        // Load the HTML file into a PDF document using the specified options.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to single‑page PDF: {pdfPath}");
    }
}