using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string xmlPath   = "temp.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Export the PDF structure to XML
            doc.Save(xmlPath, new XmlSaveOptions());

            // Load the XML, modify PageInfo.IsLandscape attribute, and save back
            XDocument xDoc = XDocument.Load(xmlPath);
            XNamespace ns = xDoc.Root.GetDefaultNamespace(); // default Aspose PDF XML namespace

            foreach (XElement pageInfo in xDoc.Descendants(ns + "PageInfo"))
            {
                pageInfo.SetAttributeValue("IsLandscape", "true");
            }

            xDoc.Save(xmlPath);

            // Bind the modified XML back to the PDF document
            doc.BindXml(xmlPath);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with landscape pages: {outputPdf}");
    }
}