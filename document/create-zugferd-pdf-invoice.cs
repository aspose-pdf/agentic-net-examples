using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "invoice_zugferd.pdf";
        const string xmlData   = "invoice.xml";          // ZUGFeRD XML file
        const string logFile   = "conversion.log";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add invoice title
            TextFragment title = new TextFragment("Invoice");
            title.TextState.FontSize = 20;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.Position = new Position(50, 800);
            page.Paragraphs.Add(title);

            // Add sample invoice details
            TextFragment details = new TextFragment(
                "Seller: Acme Corp\n" +
                "Buyer: Globex Inc\n" +
                "Amount: $1,234.56\n" +
                "Date: 2026-07-15");
            details.TextState.FontSize = 12;
            details.Position = new Position(50, 750);
            page.Paragraphs.Add(details);

            // Embed the ZUGFeRD XML data into the PDF
            if (File.Exists(xmlData))
            {
                doc.BindXml(xmlData);
            }

            // Convert the document to ZUGFeRD format (PDF with embedded XML)
            doc.Convert(logFile, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdf}'.");
    }
}