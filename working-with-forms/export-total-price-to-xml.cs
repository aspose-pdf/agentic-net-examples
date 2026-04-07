using System;
using System.IO;
using System.Linq;
using System.Xml;

class Program
{
    static void Main()
    {
        // Sample list of item prices; replace with actual calculation as needed
        decimal[] itemPrices = { 19.99m, 5.49m, 12.30m };
        decimal totalPrice = itemPrices.Sum();

        // Create an XML document representing the invoice
        XmlDocument xmlDoc = new XmlDocument();

        // XML declaration (e.g., <?xml version="1.0" encoding="utf-8"?>)
        XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        xmlDoc.AppendChild(declaration);

        // Root element <Invoice>
        XmlElement root = xmlDoc.CreateElement("Invoice");
        xmlDoc.AppendChild(root);

        // Child element <TotalPrice> containing the calculated total
        XmlElement totalElem = xmlDoc.CreateElement("TotalPrice");
        totalElem.InnerText = totalPrice.ToString("F2"); // format with two decimal places
        root.AppendChild(totalElem);

        // Save the XML to a file for external invoicing integration
        string outputPath = "total_price.xml";
        xmlDoc.Save(outputPath);

        Console.WriteLine($"Total price {totalPrice:F2} saved to '{outputPath}'.");
    }
}