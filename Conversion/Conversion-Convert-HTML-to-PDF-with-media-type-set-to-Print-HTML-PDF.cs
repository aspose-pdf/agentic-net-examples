using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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
        string pdfPath  = args[1];

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – '{htmlPath}'.");
            return;
        }

        try
        {
            // Configure loading options. The HtmlMediaTypes property does not exist in the current
            // Aspose.Pdf version, so we use the default options.
            var loadOptions = new HtmlLoadOptions();

            // Load the HTML document into an Aspose.Pdf Document object
            var pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
            
            Console.WriteLine($"HTML successfully converted to PDF and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
