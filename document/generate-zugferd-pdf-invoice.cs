using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF, the ZUGFeRD XML data, and a conversion log file
        const string outputPdf = "invoice_zugferd.pdf";
        const string xmlData   = "invoice.xml";
        const string logFile   = "conversion.log";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Add a title using a TextFragment
            TextFragment title = new TextFragment("Invoice");
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.FontSize = 20;
            title.Position = new Position(50, 800); // Position is from bottom-left
            page.Paragraphs.Add(title);

            // Add some dummy invoice details
            TextFragment details = new TextFragment(
                "Date: 2024-04-07\n" +
                "Invoice No: 12345\n" +
                "Amount: $1,000.00");
            details.TextState.Font = FontRepository.FindFont("Helvetica");
            details.TextState.FontSize = 12;
            details.Position = new Position(50, 750);
            page.Paragraphs.Add(details);

            // Embed the ZUGFeRD XML data if the file exists
            if (File.Exists(xmlData))
            {
                doc.BindXml(xmlData);
            }

            // Convert the document to ZUGFeRD format.
            // The conversion writes any validation messages to the specified log file.
            doc.Convert(logFile, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the final PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdf}'.");
    }
}