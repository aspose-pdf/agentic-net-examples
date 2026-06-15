using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ZUGFeRDExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Create a simple order object and generate ZUGFeRD XML.
            Order sampleOrder = new Order();
            sampleOrder.InvoiceNumber = "INV-1001";
            sampleOrder.InvoiceDate = DateTime.Today;
            sampleOrder.BuyerName = "Acme Corp";
            sampleOrder.TotalAmount = 1234.56m;

            string zugferdXml = GenerateZugferdXml(sampleOrder);

            // 2. Create a PDF invoice.
            using (Document pdfDocument = new Document())
            {
                // Add a page.
                Page page = pdfDocument.Pages.Add();

                // Add some invoice text.
                TextFragment tf = new TextFragment("Invoice " + sampleOrder.InvoiceNumber);
                tf.Position = new Position(100, 700);
                page.Paragraphs.Add(tf);

                // 3. Embed the ZUGFeRD XML into the PDF as an embedded file.
                using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(zugferdXml)))
                {
                    FileSpecification fileSpec = new FileSpecification("ZUGFeRD-invoice.xml", "ZUGFeRD XML");
                    fileSpec.Contents = xmlStream;
                    pdfDocument.EmbeddedFiles.Add(fileSpec);
                }

                // 4. Save the PDF.
                pdfDocument.Save("invoice_with_zugferd.pdf");
            }
        }

        // Helper method to generate a minimal ZUGFeRD XML representation.
        private static string GenerateZugferdXml(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<Invoice>");
            sb.AppendLine("  <Header>");
            sb.AppendLine("    <InvoiceNumber>" + order.InvoiceNumber + "</InvoiceNumber>");
            sb.AppendLine("    <InvoiceDate>" + order.InvoiceDate.ToString("yyyy-MM-dd") + "</InvoiceDate>");
            sb.AppendLine("  </Header>");
            sb.AppendLine("  <Buyer>");
            sb.AppendLine("    <Name>" + order.BuyerName + "</Name>");
            sb.AppendLine("  </Buyer>");
            sb.AppendLine("  <Totals>");
            sb.AppendLine("    <TotalAmount>" + order.TotalAmount.ToString("F2") + "</TotalAmount>");
            sb.AppendLine("  </Totals>");
            sb.AppendLine("</Invoice>");
            return sb.ToString();
        }
    }

    // Simple order class used for demonstration.
    public class Order
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}
