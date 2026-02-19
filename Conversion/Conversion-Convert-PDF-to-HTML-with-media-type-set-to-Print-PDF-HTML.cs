using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file path
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options (default options are sufficient)
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"Conversion completed successfully. HTML saved to '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
