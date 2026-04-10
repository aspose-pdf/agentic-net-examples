// AsposePdfApi.GlobalUsings.g.cs
global using System;
global using System.IO;
global using System.Xml;
global using Aspose.Pdf;
global using Aspose.Pdf.Facades;

// Program.cs
using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

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

            // Register namespaces that are commonly present in XMP packets
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            nsMgr.AddNamespace("pdf", "http://ns.adobe.com/pdf/1.3/");
            nsMgr.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
            nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Example 1: read the creator (dc:creator)
            XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", nsMgr);
            string creator = creatorNode?.InnerText ?? "(not set)";
            Console.WriteLine($"Creator: {creator}");

            // Example 2: read the PDF producer (pdf:Producer)
            XmlNode producerNode = xmlDoc.SelectSingleNode("//pdf:Producer", nsMgr);
            string producer = producerNode?.InnerText ?? "(not set)";
            Console.WriteLine($"Producer: {producer}");

            // Example 3: read the document title (dc:title)
            XmlNode titleNode = xmlDoc.SelectSingleNode("//dc:title/rdf:Alt/rdf:li", nsMgr);
            string title = titleNode?.InnerText ?? "(not set)";
            Console.WriteLine($"Title: {title}");
        }
    }
}
