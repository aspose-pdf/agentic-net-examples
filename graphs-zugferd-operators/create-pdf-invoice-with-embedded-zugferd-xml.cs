using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Mock order data
        Order order = new Order
        {
            OrderId = "INV-1001",
            IssueDate = DateTime.Today,
            CustomerName = "Acme Corp",
            CustomerId = "123456789",
            TotalAmount = 199.99m
        };

        // Generate ZUGFeRD XML (simplified representation)
        string zugferdXml = GenerateZugferdXml(order);

        const string pdfPath = "Invoice.pdf";

        // Create PDF invoice and embed the ZUGFeRD XML
        using (Document pdf = new Document())
        {
            // Add a page with basic invoice information
            Page page = pdf.Pages.Add();
            TextFragment title = new TextFragment($"Invoice {order.OrderId}");
            title.TextState.FontSize = 20;
            title.Position = new Position(50, 750);
            page.Paragraphs.Add(title);

            // Embed the ZUGFeRD XML as an embedded file (PDF/A‑3 requirement)
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(zugferdXml)))
            {
                // Create a FileSpecification for the XML file
                var fileSpec = new FileSpecification("ZUGFeRD-invoice.xml", "ZUGFeRD Invoice");
                fileSpec.Contents = xmlStream; // set the file data
                // MimeType property does not exist in the current Aspose.Pdf version; it is optional.
                pdf.EmbeddedFiles.Add(fileSpec); // add to the PDF portfolio
            }

            // Convert to PDF/A‑3 (required for ZUGFeRD compliance)
            pdf.Convert("conversion_log.xml", PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

            // Save the final PDF
            pdf.Save(pdfPath);
        }

        Console.WriteLine($"PDF invoice with embedded ZUGFeRD XML saved to {pdfPath}");
    }

    // Generates a minimal ZUGFeRD XML document from the order data
    static string GenerateZugferdXml(Order order)
    {
        XNamespace rsm = "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100";
        XNamespace udt = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100";

        XDocument doc = new XDocument(
            new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement(rsm + "CrossIndustryInvoice",
                new XElement(rsm + "ExchangedDocument",
                    new XElement(rsm + "ID", order.OrderId),
                    new XElement(rsm + "IssueDateTime",
                        new XElement(udt + "DateTimeString",
                            new XAttribute("format", "102"),
                            order.IssueDate.ToString("yyyyMMdd"))
                    )
                ),
                new XElement(rsm + "ApplicableHeaderTradeAgreement",
                    new XElement(rsm + "SellerTradeParty",
                        new XElement(rsm + "Name", order.CustomerName),
                        new XElement(rsm + "ID", order.CustomerId)
                    )
                ),
                new XElement(rsm + "ApplicableHeaderTradeSettlement",
                    new XElement(rsm + "InvoiceCurrencyCode", "EUR"),
                    new XElement(rsm + "GrandTotalAmount", order.TotalAmount)
                )
            )
        );

        // Return the XML including the declaration
        return doc.Declaration?.ToString() + doc.ToString();
    }

    // Simple order representation
    class Order
    {
        public string OrderId { get; set; }
        public DateTime IssueDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
