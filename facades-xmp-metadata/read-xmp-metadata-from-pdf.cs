using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf.Facades; // PdfXmpMetadata resides here

class XmpReader
{
    static void Main()
    {
        // Path to the PDF file whose XMP metadata will be read
        const string pdfPath = "input.pdf";

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the PdfXmpMetadata facade
        // PdfXmpMetadata implements IDisposable, so we use a using block
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);

            // Retrieve the whole XMP metadata as a byte array (XML format)
            byte[] rawData = xmp.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string
            string xmlContent = Encoding.UTF8.GetString(rawData);

            // Load the XML into an XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Prepare a namespace manager because XMP uses several XML namespaces
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            nsMgr.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
            nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Example: read the creator (dc:creator) – the value is inside an rdf:Bag/rdf:li element
            XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Bag/rdf:li", nsMgr);
            string creator = creatorNode?.InnerText ?? "(not set)";

            // Example: read the creation date (xmp:CreateDate)
            XmlNode createDateNode = xmlDoc.SelectSingleNode("//xmp:CreateDate", nsMgr);
            string createDate = createDateNode?.InnerText ?? "(not set)";

            // Output the extracted values
            Console.WriteLine($"Creator   : {creator}");
            Console.WriteLine($"CreateDate: {createDate}");
        }
    }
}