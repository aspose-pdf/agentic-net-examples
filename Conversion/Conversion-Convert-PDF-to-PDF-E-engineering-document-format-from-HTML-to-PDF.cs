using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";
        const string logPath  = "conversion.log";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document with default load options
            Aspose.Pdf.HtmlLoadOptions loadOptions = new Aspose.Pdf.HtmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(htmlPath, loadOptions))
            {
                // Convert to PDF/E (engineered) format.
                // In Aspose.Pdf PDF/E is represented by the PDF/X‑4 format.
                doc.Convert(
                    logPath,
                    Aspose.Pdf.PdfFormat.PDF_X_4,
                    Aspose.Pdf.ConvertErrorAction.Delete);

                // Save the converted PDF/E document
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF/E and saved as '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}