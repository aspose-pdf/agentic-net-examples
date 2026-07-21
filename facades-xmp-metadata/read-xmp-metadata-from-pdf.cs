using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;   // PdfXmpMetadata resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve the whole XMP packet as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Load the XML into an XmlDocument for querying
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmlBytes))
            {
                xmlDoc.Load(ms);
            }

            // Prepare namespace manager for common XMP prefixes
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("dc",  "http://purl.org/dc/elements/1.1/");
            ns.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
            ns.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Helper to fetch the inner text of the first node matching an XPath
            string GetNodeValue(string xpath)
            {
                XmlNode node = xmlDoc.SelectSingleNode(xpath, ns);
                return node?.InnerText ?? "(not found)";
            }

            // Example: read creator, title and creation date from the XMP packet
            string creator    = GetNodeValue("//dc:creator/rdf:Seq/rdf:li");
            string title      = GetNodeValue("//dc:title/rdf:Alt/rdf:li");
            string createDate = GetNodeValue("//xmp:CreateDate");

            Console.WriteLine($"Creator   : {creator}");
            Console.WriteLine($"Title     : {title}");
            Console.WriteLine($"CreateDate: {createDate}");
        }
    }
}