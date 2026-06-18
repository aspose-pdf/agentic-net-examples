using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple invoice PDF (self‑contained sample)
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            TextFragment tf = new TextFragment("Invoice #12345\nDate: 2023-10-01\nTotal: $1,000.00");
            tf.Position = new Position(50, 750);
            page.Paragraphs.Add(tf);
            sampleDoc.Save("input.pdf");
        }

        // Step 2: Load the PDF and embed ZUGFeRD XML data
        using (Document invoiceDoc = new Document("input.pdf"))
        {
            // Sample ZUGFeRD XML (normally generated according to the standard)
            string zugferdXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<Invoice>
    <ID>12345</ID>
    <IssueDate>2023-10-01</IssueDate>
    <DocumentCurrencyCode>USD</DocumentCurrencyCode>
    <LegalMonetaryTotal>
        <PayableAmount currencyID=""USD"">1000.00</PayableAmount>
    </LegalMonetaryTotal>
</Invoice>";

            byte[] xmlBytes = Encoding.UTF8.GetBytes(zugferdXml);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                FileSpecification fileSpec = new FileSpecification("ZUGFeRD-invoice.xml", "ZUGFeRD XML invoice data");
                fileSpec.Contents = xmlStream;
                invoiceDoc.EmbeddedFiles.Add(fileSpec);
            }

            invoiceDoc.Save("output.pdf");
        }
    }
}
