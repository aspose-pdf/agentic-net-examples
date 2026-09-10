using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class XmpNamespaceDiagnostic
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and retrieve its XMP metadata as XML bytes
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            byte[] rawData = xmp.GetXmpMetadata();

            if (rawData == null || rawData.Length == 0)
            {
                Console.WriteLine("No XMP metadata found in the PDF.");
                return;
            }

            // Load the XML into an XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(rawData))
            {
                xmlDoc.Load(ms);
            }

            // The XMP metadata root element typically contains namespace declarations
            XmlElement root = xmlDoc.DocumentElement;
            if (root == null)
            {
                Console.WriteLine("Unable to parse XMP metadata XML.");
                return;
            }

            Console.WriteLine("XMP Namespaces present in the PDF:");
            // Iterate over all attributes of the root element to find xmlns declarations
            foreach (XmlAttribute attr in root.Attributes)
            {
                if (attr.Prefix == "xmlns")
                {
                    // Attribute of form xmlns:prefix="uri"
                    string prefix = attr.LocalName; // the prefix part after xmlns:
                    string uri = attr.Value;
                    Console.WriteLine($"Prefix: '{prefix}'  URI: '{uri}'");
                }
                else if (attr.Name == "xmlns")
                {
                    // Default namespace declaration xmlns="uri"
                    string uri = attr.Value;
                    Console.WriteLine($"Default namespace URI: '{uri}'");
                }
            }
        }
    }
}