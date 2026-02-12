using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file path (adjust as needed)
        const string htmlPath = "input.html";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load options for HTML -> PDF conversion
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML document into an Aspose.Pdf Document object
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the document as PDF
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF and saved at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
