using System;
using System.IO;
using Aspose.Pdf;   // Provides Document, HtmlLoadOptions, etc.

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file and desired PDF output file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions can be customized if needed.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a PDF document from the HTML source.
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF. This follows the prescribed document-save rule.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}