using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file (ZUGFeRD compliant)
        const string outputPdf = "invoice_zugferd.pdf";

        // Path to the ZUGFeRD XML data that will be embedded
        const string xmlData = "invoice.xml";

        // Optional log file for the conversion process
        const string logFile = "conversion.log";

        // Create a simple ZUGFeRD XML file if it does not exist (demo purposes)
        if (!File.Exists(xmlData))
        {
            string sampleXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<Invoice>
  <ID>INV-1001</ID>
  <IssueDate>2023-12-01</IssueDate>
  <Seller>
    <Name>Acme Corp</Name>
    <TaxID>123456789</TaxID>
  </Seller>
  <Buyer>
    <Name>Beta Ltd</Name>
    <TaxID>987654321</TaxID>
  </Buyer>
  <TotalAmount currency=""EUR"">1500.00</TotalAmount>
</Invoice>";
            File.WriteAllText(xmlData, sampleXml);
        }

        // Create a new PDF document and add basic invoice content
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add a text fragment with invoice details
            TextFragment tf = new TextFragment(
                "Invoice #INV-1001\n" +
                "Date: 2023-12-01\n" +
                "Seller: Acme Corp\n" +
                "Buyer: Beta Ltd\n" +
                "Total: EUR 1500.00");
            tf.Position = new Position(50, 750); // Position on the page
            page.Paragraphs.Add(tf);

            // Bind the ZUGFeRD XML data to the PDF
            doc.BindXml(xmlData);

            // Convert the document to ZUGFeRD format (PDF/A‑3 with embedded XML)
            // ConvertErrorAction.Delete removes objects that cannot be converted
            doc.Convert(logFile, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

            // Save the resulting PDF (the document is now ZUGFeRD compliant)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdf}'.");
    }
}