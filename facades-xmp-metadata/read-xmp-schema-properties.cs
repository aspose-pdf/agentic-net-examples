using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        xmpMetadata.BindPdf(inputPath);
        byte[] rawData = xmpMetadata.GetXmpMetadata();

        string xmlContent = Encoding.UTF8.GetString(rawData);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
        nsMgr.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");

        XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", nsMgr);
        XmlNode createDateNode = xmlDoc.SelectSingleNode("//xmp:CreateDate", nsMgr);

        string creator = creatorNode != null ? creatorNode.InnerText : "N/A";
        string createDate = createDateNode != null ? createDateNode.InnerText : "N/A";

        Console.WriteLine("Creator: " + creator);
        Console.WriteLine("CreateDate: " + createDate);
    }
}
