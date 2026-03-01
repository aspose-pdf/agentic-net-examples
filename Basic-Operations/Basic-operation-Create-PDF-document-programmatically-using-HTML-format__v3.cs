using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input HTML file path (source document in HTML format)
        const string htmlPath = "input.html";

        // Output PDF file path (resulting PDF document)
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file into a Document using HtmlLoadOptions.
            // HtmlLoadOptions can be customized if needed (e.g., base path, font embedding, etc.).
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // The Document constructor with (string, LoadOptions) loads the HTML content.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the loaded content as a PDF file.
                // No SaveOptions are required for PDF output; the format is inferred from the file extension.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., parsing issues, I/O problems)
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}