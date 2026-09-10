using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and retrieve its XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);

            // Get the complete XMP metadata as XML bytes
            byte[] fullXmlBytes = xmp.GetXmpMetadata();
            string fullXml = Encoding.UTF8.GetString(fullXmlBytes);

            // Load the XML into an XmlDocument for parsing
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fullXml);

            // Prepare a namespace manager with common XMP prefixes
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            nsMgr.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
            nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Example: read the first dc:creator value (may be inside rdf:Seq/rdf:li)
            XmlNode creatorNode = doc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", nsMgr);
            string creator = creatorNode?.InnerText ?? "(none)";
            Console.WriteLine($"Creator: {creator}");

            // Example: read the xmp:CreateDate property
            XmlNode createDateNode = doc.SelectSingleNode("//xmp:CreateDate", nsMgr);
            string createDate = createDateNode?.InnerText ?? "(none)";
            Console.WriteLine($"CreateDate: {createDate}");

            // Alternative approach: retrieve a specific part directly via GetXmpMetadata(string)
            byte[] creatorPartBytes = xmp.GetXmpMetadata("dc:creator");
            string creatorPartXml = Encoding.UTF8.GetString(creatorPartBytes);
            Console.WriteLine($"Raw dc:creator XML fragment: {creatorPartXml}");
        }
    }
}