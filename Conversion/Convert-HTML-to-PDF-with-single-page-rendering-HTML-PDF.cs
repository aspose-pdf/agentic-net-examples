using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, HtmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input HTML file path.
        const string htmlPath = "input.html";

        // Desired output PDF file path.
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Configure loading options to render the entire HTML document onto a single PDF page.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                IsRenderToSinglePage = true
            };

            // Load the HTML file into a PDF Document using the specified options.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the document as PDF. No SaveOptions are required for PDF output.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to single‑page PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., parsing issues, I/O problems).
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}