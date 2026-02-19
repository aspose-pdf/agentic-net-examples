using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument) and output PDF file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: HtmlToPdfConverter <input.html> <output.pdf>");
            return;
        }

        string htmlPath = args[0];
        string pdfPath = args[1];

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using Aspose.Pdf's HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the loaded document as PDF
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}