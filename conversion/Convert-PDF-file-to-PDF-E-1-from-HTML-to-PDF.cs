using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlPath = "input.html";
        const string pdfPath = "output_pdfe1.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML source not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file into a PDF document
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Convert the document to PDF/E‑1 format.
                // The conversion writes a log file with any conversion issues.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted document. The resulting file complies with PDF/E‑1.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF/E‑1: {pdfPath}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}