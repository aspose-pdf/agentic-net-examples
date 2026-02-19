using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "sample.html";
        const string pdfPath  = "sample.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document.
            // HtmlLoadOptions provides options for HTML → PDF conversion.
            // The CompressSvgImages flag is not available in the current Aspose.Pdf version,
            // so we use the default options.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a Document object from the HTML source using the specified options.
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF file.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"Conversion completed successfully. PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
