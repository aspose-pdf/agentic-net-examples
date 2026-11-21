using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlOutputPath = "total_price.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and extract all text
        using (Document pdfDoc = new Document(pdfPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string allText = absorber.Text;

            // Simple regex to find a monetary amount after the word "Total"
            // Adjust the pattern according to the actual invoice layout if needed
            Match match = Regex.Match(allText, @"Total\s*[:\-]?\s*\$?([\d,]+(?:\.\d{2})?)",
                                      RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                Console.Error.WriteLine("Total price not found in the PDF.");
                return;
            }

            string totalPrice = match.Groups[1].Value;

            // Build the XML document
            XDocument xmlDoc = new XDocument(
                new XElement("Invoice",
                    new XElement("TotalPrice", totalPrice)
                )
            );

            // Save the XML file
            xmlDoc.Save(xmlOutputPath);
            Console.WriteLine($"Total price exported to XML: {xmlOutputPath}");
        }
    }
}