using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input HTML file and output PDF file
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: HtmlToPdfConverter <input.html> <output.pdf>");
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
            // Load the HTML document with default options (no PreserveStructure property in current version)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a PDF document from the HTML source
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
            Console.WriteLine($"PDF successfully saved to: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
