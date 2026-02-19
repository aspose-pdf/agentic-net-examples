using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf; // HtmlLoadOptions resides in this namespace

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions.
            // HtmlLoadOptions can be customized if needed (e.g., BasePath, IsRenderToSinglePage).
            var loadOptions = new HtmlLoadOptions();

            // Create a PDF Document from the HTML source.
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF document.
            // This uses the standard document-save rule.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}