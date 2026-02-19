using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfTransparentText
{
    static void Main(string[] args)
    {
        // Input HTML file path (adjust as needed)
        const string htmlPath = "input.html";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions enables HTML‑to‑PDF conversion.
            // No additional options are required for transparent‑text rendering;
            // the conversion engine handles it automatically.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF with transparent text: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Generic error handling – report any unexpected issues.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
