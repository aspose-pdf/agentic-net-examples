using System;
using System.IO;
using Aspose.Pdf; // HtmlLoadOptions and Document are in this namespace

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";

        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions.
            // HtmlLoadOptions can be customized if needed (e.g., BasePath, IsRenderToSinglePage, etc.).
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a Document instance from the HTML source.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the document as PDF.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}