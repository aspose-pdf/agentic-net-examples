using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Order
{
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string Buyer { get; set; }
    public string Seller { get; set; }
    public decimal TotalAmount { get; set; }
    // Add other order fields as needed
}

class Program
{
    static void Main()
    {
        // Sample order data
        Order order = new Order
        {
            InvoiceNumber = "INV-2023-001",
            InvoiceDate   = DateTime.Today,
            Buyer         = "Acme Corp.",
            Seller        = "Widget Ltd.",
            TotalAmount   = 1234.56m
        };

        // Generate ZUGFeRD XML (simplified example)
        XDocument zugferdXml = new XDocument(
            new XElement("Invoice",
                new XElement("InvoiceNumber", order.InvoiceNumber),
                new XElement("InvoiceDate", order.InvoiceDate.ToString("yyyy-MM-dd")),
                new XElement("Buyer", order.Buyer),
                new XElement("Seller", order.Seller),
                new XElement("TotalAmount", order.TotalAmount)
            )
        );

        // Convert XML to a memory stream (UTF-8)
        using (MemoryStream xmlStream = new MemoryStream())
        {
            zugferdXml.Save(xmlStream);
            xmlStream.Position = 0; // Reset for reading

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a simple page with invoice details
                Page page = pdfDoc.Pages.Add();
                TextFragment tf = new TextFragment($"Invoice: {order.InvoiceNumber}\nDate: {order.InvoiceDate:d}\nBuyer: {order.Buyer}\nSeller: {order.Seller}\nTotal: {order.TotalAmount:C}");
                tf.Position = new Position(50, 750); // Top-left position
                page.Paragraphs.Add(tf);

                // Attach the ZUGFeRD XML to the PDF
                pdfDoc.BindXml(xmlStream);

                // Save the PDF invoice
                string outputPath = "Invoice_With_ZUGFeRD.pdf";
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF invoice saved to '{outputPath}'.");
            }
        }
    }
}