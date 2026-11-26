using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfOutput = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xdoc = XDocument.Load(xmlPath);

        // Find the first PageInfo element
        XElement pageInfo = xdoc.Descendants("PageInfo").FirstOrDefault();
        if (pageInfo == null)
        {
            Console.Error.WriteLine("PageInfo element not found in the XML.");
            return;
        }

        // Adjust margin attributes (example values)
        pageInfo.SetAttributeValue("MarginLeft",   "20");
        pageInfo.SetAttributeValue("MarginRight",  "20");
        pageInfo.SetAttributeValue("MarginTop",    "20");
        pageInfo.SetAttributeValue("MarginBottom", "20");

        // Save the modified XML into a memory stream
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xdoc.Save(xmlStream);
            xmlStream.Position = 0; // Reset stream position for reading

            // Load PDF from the modified XML
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document pdfDoc = new Document(xmlStream, loadOptions))
            {
                // Save the bound PDF
                pdfDoc.Save(pdfOutput);
            }
        }

        Console.WriteLine($"PDF successfully saved to '{pdfOutput}'.");
    }
}