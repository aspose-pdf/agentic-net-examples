using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ZugferdInvoiceGenerator
{
    static void Main()
    {
        const string outputPdf = "Invoice_ZUGFeRD.pdf";

        // Sample ZUGFeRD XML data (normally generated according to the ZUGFeRD schema)
        string zugferdXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<rsm:CrossIndustryInvoice xmlns:rsm=""urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"" xmlns:ram=""urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"">
  <rsm:ExchangedDocumentContext>
    <ram:GuidelineSpecifiedDocumentContextParameter>
      <ram:ID>urn:ferd:CrossIndustryDocument:invoice:1p0:basic</ram:ID>
    </ram:GuidelineSpecifiedDocumentContextParameter>
  </rsm:ExchangedDocumentContext>
  <rsm:ExchangedDocument>
    <ram:ID>INV-2023-001</ram:ID>
    <ram:IssueDateTime>2023-09-01</ram:IssueDateTime>
    <ram:TypeCode>380</ram:TypeCode>
  </rsm:ExchangedDocument>
</rsm:CrossIndustryInvoice>";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a title paragraph
            TextFragment title = new TextFragment("Invoice")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.Black }
            };
            page.Paragraphs.Add(title);

            // Add invoice details
            TextFragment details = new TextFragment(
                "Invoice Number: INV-2023-001\n" +
                "Date: 2023-09-01\n" +
                "Seller: Acme Corp.\n" +
                "Buyer: Global Industries\n" +
                "Total Amount: $1,250.00")
            {
                TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.DarkGray }
            };
            page.Paragraphs.Add(details);

            // Embed the ZUGFeRD XML payload into the PDF as an embedded file (AFRelationship.Data)
            using (MemoryStream xmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(zugferdXml)))
            {
                var fileSpec = new FileSpecification(xmlStream, "invoice.xml", "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF invoice
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"ZUGFeRD‑compliant invoice saved to '{outputPdf}'.");
    }
}
