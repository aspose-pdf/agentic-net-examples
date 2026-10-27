using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample order data
        var order = new Order
        {
            Id = "ORD-001",
            IssueDate = DateTime.Today,
            Buyer = "Acme Corp",
            Seller = "Widget Ltd",
            TotalAmount = 1234.56m
        };

        // Generate ZUGFeRD XML from the order
        XDocument zugferdXml = GenerateZugferdXml(order);

        // Write XML into a memory stream
        using (MemoryStream xmlStream = new MemoryStream())
        {
            zugferdXml.Save(xmlStream);
            xmlStream.Position = 0;

            // Create a new PDF invoice
            using (Document pdfDoc = new Document())
            {
                // Add a page to the document
                Page page = pdfDoc.Pages.Add();

                // Invoice title
                TextFragment title = new TextFragment("Invoice");
                title.Position = new Position(50, 750);
                title.TextState.FontSize = 24;
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.TextState.ForegroundColor = Color.Black;
                page.Paragraphs.Add(title);

                // Order details
                TextFragment details = new TextFragment(
                    $"Order ID: {order.Id}\nDate: {order.IssueDate:yyyy-MM-dd}\nBuyer: {order.Buyer}\nSeller: {order.Seller}\nTotal: {order.TotalAmount:C}");
                details.Position = new Position(50, 700);
                details.TextState.FontSize = 12;
                details.TextState.Font = FontRepository.FindFont("Helvetica");
                page.Paragraphs.Add(details);

                // Attach the ZUGFeRD XML as an embedded file (PDF/A‑3B compliant)
                var fileSpec = new FileSpecification(xmlStream, "ZUGFeRD.xml", "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Convert to PDF/A‑3B (required for ZUGFeRD compliance)
                pdfDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the PDF
                pdfDoc.Save("Invoice_With_ZUGFeRD.pdf");
            }
        }

        Console.WriteLine("PDF invoice with ZUGFeRD XML created.");
    }

    // Generates a minimal ZUGFeRD XML document for the given order
    static XDocument GenerateZugferdXml(Order order)
    {
        XNamespace ns = "urn:ferd:CrossIndustryDocument:invoice:1p0";
        return new XDocument(
            new XElement(ns + "CrossIndustryDocument",
                new XElement(ns + "Header",
                    new XElement(ns + "ID", order.Id),
                    new XElement(ns + "IssueDateTime", order.IssueDate.ToString("yyyy-MM-dd"))
                ),
                new XElement(ns + "SellerTradeParty",
                    new XElement(ns + "Name", order.Seller)
                ),
                new XElement(ns + "BuyerTradeParty",
                    new XElement(ns + "Name", order.Buyer)
                ),
                new XElement(ns + "SpecifiedTradeSettlementHeaderMonetarySummation",
                    new XElement(ns + "GrandTotalAmount", order.TotalAmount)
                )
            )
        );
    }

    // Simple POCO representing an order
    class Order
    {
        public string Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string Buyer { get; set; }
        public string Seller { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
