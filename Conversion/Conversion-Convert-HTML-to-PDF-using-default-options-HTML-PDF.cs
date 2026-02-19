using System;
using System.IO;
using Aspose.Pdf; // Provides Document and HtmlLoadOptions

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file and output PDF file paths
        string htmlPath = "input.html";
        string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using default load options
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the document as PDF (uses the provided document-save rule)
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}