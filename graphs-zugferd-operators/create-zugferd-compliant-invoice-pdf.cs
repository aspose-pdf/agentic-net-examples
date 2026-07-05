using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for output files
        const string outputPdfPath = "ZugferdInvoice.pdf";

        // Sample XML data for ZUGFeRD (normally generated according to the standard)
        string zugferdXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<rsm:CrossIndustryDocument xmlns:rsm=""urn:ferd:CrossIndustryDocument:invoice:1p0"" xmlns:ram=""urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"">
  <rsm:ExchangedDocumentContext>
    <ram:GuidelineSpecifiedDocumentContextParameter>
      <ram:ID>urn:ferd:CrossIndustryDocument:invoice:1p0:basic</ram:ID>
    </ram:GuidelineSpecifiedDocumentContextParameter>
  </rsm:ExchangedDocumentContext>
  <rsm:ExchangedDocument>
    <ram:ID>INV-2023-001</ram:ID>
    <ram:Name>Invoice</ram:Name>
    <ram:TypeCode>380</ram:TypeCode>
    <ram:IssueDateTime>2023-01-15</ram:IssueDateTime>
  </rsm:ExchangedDocument>
  <!-- Additional required elements would go here -->
</rsm:CrossIndustryDocument>";

        // Create a memory stream for the XML
        using (MemoryStream xmlStream = new MemoryStream())
        using (StreamWriter writer = new StreamWriter(xmlStream))
        {
            writer.Write(zugferdXml);
            writer.Flush();
            xmlStream.Position = 0; // Reset stream position for reading

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a page
                Page page = pdfDoc.Pages.Add();

                // Add readable invoice content
                TextFragment header = new TextFragment("Invoice #INV-2023-001")
                {
                    TextState = { FontSize = 18, Font = FontRepository.FindFont("Helvetica-Bold"), ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(header);

                TextFragment body = new TextFragment("Date: 2023-01-15\nCustomer: ACME Corp.\nAmount: $1,250.00")
                {
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(body);

                // Bind the ZUGFeRD XML to the PDF (embeds it as an attachment and sets required entries)
                pdfDoc.BindXml(xmlStream);

                // Save the final ZUGFeRD‑compliant PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdfPath}'.");
    }
}