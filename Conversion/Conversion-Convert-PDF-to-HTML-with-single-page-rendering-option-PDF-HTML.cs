using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output HTML file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToHtmlConverter <input.pdf> <output.html>");
            return;
        }

        string pdfPath = args[0];
        string htmlPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Convert and save to HTML.
            // Using the overload without explicit options applies the default HtmlSaveOptions,
            // which renders the entire PDF into a single HTML page.
            pdfDocument.Save(htmlPath);
            
            Console.WriteLine($"Conversion successful. HTML saved to '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}