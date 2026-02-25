using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlInputPath = "input.html";
        const string pdfOutputPath = "output.pdf";
        const string conversionLogPath = "conversion_log.txt";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML file. HtmlLoadOptions is required for HTML‑to‑PDF conversion.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document doc = new Document(htmlInputPath, loadOptions))
            {
                // Convert the document to PDF/E‑1. The conversion logs any errors to the specified file.
                // PdfFormat.PDF_E_1 represents the PDF/E‑1 standard.
                doc.Convert(conversionLogPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the resulting PDF/E‑1 file.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF/E‑1: '{pdfOutputPath}'.");
        }
        // HTML‑to‑PDF conversion relies on GDI+, which is Windows‑only.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}