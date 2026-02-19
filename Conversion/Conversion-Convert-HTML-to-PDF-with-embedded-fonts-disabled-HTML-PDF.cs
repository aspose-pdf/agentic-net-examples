using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Define input HTML and output PDF paths
        string dataDir = Directory.GetCurrentDirectory(); // use current directory as base
        string htmlPath = Path.Combine(dataDir, "input.html");
        string pdfPath  = Path.Combine(dataDir, "output.pdf");

        // Verify that the HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML file with font embedding disabled
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                IsEmbedFonts = false   // do not embed fonts into the resulting PDF
            };

            // Create a PDF document from the HTML source
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the PDF document
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF. Output saved at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}