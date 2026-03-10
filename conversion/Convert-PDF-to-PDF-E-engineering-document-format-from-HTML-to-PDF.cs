using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input HTML and output files
        const string htmlPath   = "input.html";
        const string pdfPath    = "intermediate.pdf";
        const string pdfEPath   = "output_pdfe.pdf";
        const string logPath    = "conversion_log.txt";

        // Verify the HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file into a PDF document
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Optional: save the intermediate PDF (useful for debugging)
            doc.Save(pdfPath);

            // Convert the document to PDF/E format.
            // The Convert method writes conversion errors to the specified log file.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the final PDF/E document
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF/E: {pdfEPath}");
    }
}