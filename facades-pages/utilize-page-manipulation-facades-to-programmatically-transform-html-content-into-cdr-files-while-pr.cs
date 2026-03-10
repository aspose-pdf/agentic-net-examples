using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HtmlToCdrConverter
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";

        // Intermediate PDF file path (Aspose.Pdf can convert HTML to PDF)
        const string pdfPath = "intermediate.pdf";

        // Desired output CDR file path (CorelDRAW format)
        const string cdrPath = "output.cdr";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions.
            // HtmlLoadOptions is a class, not a namespace, so we instantiate it directly.
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Save the loaded HTML as PDF to preserve layout and styles.
                // This uses the standard Document.Save method (PDF is the default format).
                doc.Save(pdfPath);
            }

            // NOTE:
            // Aspose.Pdf does NOT provide a CDR (CorelDRAW) export option.
            // The library can convert HTML to PDF, but converting PDF to CDR is not supported.
            // If CDR output is required, consider using a dedicated CorelDRAW SDK or another tool
            // that can import PDF and export to CDR.

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
            Console.WriteLine("CDR export is not supported by Aspose.Pdf.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}