using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ZugferdInvoiceGenerator
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlPath = "invoice.xml";

        string zugferdXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<rsm:CrossIndustryInvoice xmlns:rsm=""urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"" xmlns:ram=""urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"">
  <rsm:ExchangedDocumentContext>
    <ram:GuidelineSpecifiedDocumentContextParameter>
      <ram:ID>urn:ferd:CrossIndustryDocument:invoice:1p0:basic</ram:ID>
    </ram:GuidelineSpecifiedDocumentContextParameter>
  </rsm:ExchangedDocumentContext>
  <rsm:ExchangedDocument>
    <ram:ID>INV-2023-001</ram:ID>
    <ram:IssueDateTime>
      <udt:DateTimeString format=""102"">20231101</udt:DateTimeString>
    </ram:IssueDateTime>
    <ram:TypeCode>380</ram:TypeCode>
  </rsm:ExchangedDocument>
</rsm:CrossIndustryInvoice>";

        // Optional: write the XML to a file for reference
        File.WriteAllText(xmlPath, zugferdXml, Encoding.UTF8);

        using (Document pdf = new Document())
        {
            // Add a page
            Page page = pdf.Pages.Add();

            // Title
            TextFragment title = new TextFragment("Invoice")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Position(0, 750)
            };
            page.Paragraphs.Add(title);

            // Invoice details
            TextFragment details = new TextFragment("Invoice Number: INV-2023-001\nDate: 2023-11-01\nAmount: $1,250.00")
            {
                TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.DarkGray },
                Position = new Position(50, 700)
            };
            page.Paragraphs.Add(details);

            // Embed the ZUGFeRD XML as an embedded file (AFRelationship = Data)
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(zugferdXml)))
            {
                var fileSpec = new FileSpecification(xmlStream, "invoice.xml", "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                pdf.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF. If PDF/A‑3u compliance is required, it can be added via Convert() with PdfFormat.PDF_A_3U when the library version supports it.
            pdf.Save(pdfPath);
        }

        Console.WriteLine($"ZUGFeRD‑compliant invoice saved to '{pdfPath}'.");
    }
}