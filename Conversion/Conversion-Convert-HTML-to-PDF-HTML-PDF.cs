using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument) and output PDF path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: HtmlToPdfConverter <input.html> <output.pdf>");
            return;
        }

        string htmlPath = args[0];
        string pdfPath = args[1];

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions
            var loadOptions = new HtmlLoadOptions();               // No special options required
            Document pdfDocument = new Document(htmlPath, loadOptions); // Create and load

            // Save the document as PDF (simple save without additional options)
            pdfDocument.Save(pdfPath);                              // document-save rule

            Console.WriteLine($"HTML successfully converted to PDF and saved at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}