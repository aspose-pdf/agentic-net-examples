using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "invoice.pdf";
        const string xmlOutputPath = "total_price.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Extract all text from the PDF (text extraction rule)
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string allText = absorber.Text;

            // Find all monetary values (e.g., 123.45, $123.45) using a regex
            // Adjust the pattern to match the format used in your invoices
            Regex priceRegex = new Regex(@"\b\d{1,3}(?:,\d{3})*(?:\.\d{2})?\b");
            MatchCollection matches = priceRegex.Matches(allText);

            decimal total = 0m;
            foreach (Match m in matches)
            {
                // Remove any thousand separators and parse
                string clean = m.Value.Replace(",", "");
                if (decimal.TryParse(clean, out decimal value))
                {
                    total += value;
                }
            }

            // Build a simple XML document with the total price
            XmlDocument xmlDoc = new XmlDocument();

            // Create XML declaration
            XmlDeclaration decl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(decl);

            // Root element <Invoice>
            XmlElement root = xmlDoc.CreateElement("Invoice");
            xmlDoc.AppendChild(root);

            // Child element <TotalPrice>
            XmlElement totalElem = xmlDoc.CreateElement("TotalPrice");
            totalElem.InnerText = total.ToString("F2"); // two decimal places
            root.AppendChild(totalElem);

            // Save the XML file (lifecycle rule: use standard Save method)
            xmlDoc.Save(xmlOutputPath);
        }

        Console.WriteLine($"Total price exported to XML: {xmlOutputPath}");
    }
}