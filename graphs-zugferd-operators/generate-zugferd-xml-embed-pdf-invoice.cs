using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";

        var order = new
        {
            Id = "INV-1001",
            IssueDate = DateTime.Today,
            Buyer = new { Name = "Acme Corp", TaxId = "1234567890" },
            Seller = new { Name = "My Company", TaxId = "0987654321" },
            TotalAmount = 1999.99m,
            Currency = "EUR"
        };

        // Build ZUGFeRD XML (simplified example)
        XDocument zugferdXml = new XDocument(
            new XElement("Invoice",
                new XElement("ID", order.Id),
                new XElement("IssueDate", order.IssueDate.ToString("yyyy-MM-dd")),
                new XElement("Buyer",
                    new XElement("Name", order.Buyer.Name),
                    new XElement("TaxID", order.Buyer.TaxId)
                ),
                new XElement("Seller",
                    new XElement("Name", order.Seller.Name),
                    new XElement("TaxID", order.Seller.TaxId)
                ),
                new XElement("TotalAmount",
                    new XAttribute("currencyID", order.Currency),
                    order.TotalAmount
                )
            )
        );

        // Convert XML to a memory stream for attachment
        using (MemoryStream xmlStream = new MemoryStream())
        {
            zugferdXml.Save(xmlStream);
            xmlStream.Position = 0; // reset stream position

            // Create a new PDF document (invoice)
            using (Document pdfDoc = new Document())
            {
                // Add a page and simple invoice text
                Page page = pdfDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment($"Invoice #{order.Id}"));
                page.Paragraphs.Add(new TextFragment($"Date: {order.IssueDate:yyyy-MM-dd}"));
                page.Paragraphs.Add(new TextFragment($"Buyer: {order.Buyer.Name}"));
                page.Paragraphs.Add(new TextFragment($"Seller: {order.Seller.Name}"));
                page.Paragraphs.Add(new TextFragment($"Total: {order.TotalAmount} {order.Currency}"));

                // Embed the ZUGFeRD XML as an embedded file
                var fileSpec = new FileSpecification("ZUGFeRD-invoice.xml", "ZUGFeRD Invoice");
                fileSpec.Contents = xmlStream; // assign the stream containing the XML
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the PDF invoice
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF invoice with embedded ZUGFeRD XML saved to '{pdfPath}'.");
    }
}
