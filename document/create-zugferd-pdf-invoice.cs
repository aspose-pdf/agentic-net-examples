using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "invoice_zugferd.pdf";
        const string xmlData   = "invoice.xml";
        const string logFile   = "convert.log";

        // Ensure the ZUGFeRD XML file exists; create a minimal placeholder if missing.
        if (!File.Exists(xmlData))
        {
            File.WriteAllText(xmlData, "<Invoice></Invoice>");
        }

        // Use absolute paths – Aspose.PDF expects a URI‑compatible string for BindXml and Convert.
        string xmlPath  = Path.GetFullPath(xmlData);
        string pdfPath  = Path.GetFullPath(outputPdf);
        string logPath  = Path.GetFullPath(logFile);

        // Create a new PDF document inside a using block for proper disposal.
        using (Document doc = new Document())
        {
            // Add a single page to the document.
            Page page = doc.Pages.Add();

            // Add a title to the invoice.
            TextFragment title = new TextFragment("Invoice");
            title.TextState.FontSize = 20;
            title.Position = new Position(50, 800);
            page.Paragraphs.Add(title);

            // Add some sample invoice details.
            TextFragment details = new TextFragment(
                "Customer: Acme Corp\n" +
                "Amount: $1,234.56\n" +
                "Date: 2026-07-02");
            details.Position = new Position(50, 750);
            page.Paragraphs.Add(details);

            // Bind the ZUGFeRD XML data to the PDF. Use the absolute file path to avoid UriFormatException.
            doc.BindXml(xmlPath);

            // Convert the PDF to ZUGFeRD (PDF/A‑3U with embedded XML).
            // The Convert method returns a boolean indicating success; we ignore it here.
            doc.Convert(logPath, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the final PDF invoice.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdf}'.");
    }
}
