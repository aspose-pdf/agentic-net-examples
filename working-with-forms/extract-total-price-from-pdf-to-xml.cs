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
        const string inputPdfPath = "invoice.pdf";      // source PDF containing line items
        const string outputXmlPath = "total_price.xml"; // XML file for external system

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Extract all text from the document
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages.Accept(absorber);
                string allText = absorber.Text ?? string.Empty;

                // Find all monetary values (e.g., $123.45 or 123.45) and sum them
                decimal total = 0m;
                // Pattern matches optional currency symbol followed by digits, optional decimal part
                Regex priceRegex = new Regex(@"\$?\b\d{1,3}(?:,\d{3})*(?:\.\d{2})?\b", RegexOptions.Compiled);
                foreach (Match match in priceRegex.Matches(allText))
                {
                    // Remove any commas and currency symbols before parsing
                    string numericPart = match.Value.Replace("$", "").Replace(",", "");
                    if (decimal.TryParse(numericPart, out decimal value))
                    {
                        total += value;
                    }
                }

                // Create a simple XML document with the calculated total price
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("InvoiceSummary");
                xmlDoc.AppendChild(root);

                XmlElement totalElement = xmlDoc.CreateElement("TotalPrice");
                totalElement.InnerText = total.ToString("F2"); // two decimal places
                root.AppendChild(totalElement);

                // Save the XML to the specified file
                xmlDoc.Save(outputXmlPath);
                Console.WriteLine($"Total price ({total:F2}) exported to '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}