using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice_zugferd.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the ZUGFeRD PDF
        using (Document doc = new Document(pdfPath))
        {
            // Attempt to retrieve the embedded invoice XML.
            // ZUGFeRD PDFs commonly store the XML under the catalog key "ZUGFeRDXML".
            // Adjust the key if the PDF uses a different entry.
            string xmlContent = doc.GetCatalogValue("ZUGFeRDXML") as string;

            if (string.IsNullOrEmpty(xmlContent))
            {
                Console.Error.WriteLine("Embedded ZUGFeRD XML not found in the PDF catalog.");
                return;
            }

            // Parse the XML content
            XDocument invoiceXml;
            try
            {
                invoiceXml = XDocument.Parse(xmlContent);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse embedded XML: {ex.Message}");
                return;
            }

            // Example processing: extract the Invoice Number (BT-1) and Invoice Date (BT-2)
            XNamespace ns = "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100";
            var invoiceNumber = invoiceXml.Root
                ?.Descendants(ns + "BT-1")
                ?.FirstOrDefault()
                ?.Value ?? "N/A";

            var invoiceDate = invoiceXml.Root
                ?.Descendants(ns + "BT-2")
                ?.FirstOrDefault()
                ?.Value ?? "N/A";

            Console.WriteLine($"Invoice Number: {invoiceNumber}");
            Console.WriteLine($"Invoice Date:   {invoiceDate}");

            // Additional programmatic processing can be performed here,
            // such as iterating over line items, totals, etc.
        }
    }
}
