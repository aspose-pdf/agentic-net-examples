using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class XmpNamespaceLister
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load XMP metadata using the PdfXmpMetadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve the raw XMP packet as a byte array
            byte[] xmpData = xmp.GetXmpMetadata();

            // Load the XMP XML into an XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmpData))
            {
                xmlDoc.Load(ms);
            }

            // The root element typically contains the namespace declarations (xmlns:prefix="uri")
            XmlElement root = xmlDoc.DocumentElement;
            if (root == null)
            {
                Console.WriteLine("No XMP metadata found.");
                return;
            }

            Console.WriteLine("XMP Namespaces found in the PDF:");
            foreach (XmlAttribute attr in root.Attributes)
            {
                // Namespace declarations are either xmlns="defaultUri" or xmlns:prefix="uri"
                if (attr.Prefix == "xmlns")
                {
                    // Example: xmlns:dc="http://purl.org/dc/elements/1.1/"
                    Console.WriteLine($"{attr.LocalName} = {attr.Value}");
                }
                else if (attr.Name == "xmlns")
                {
                    // Default namespace declaration
                    Console.WriteLine($"default = {attr.Value}");
                }
            }
        }
    }
}