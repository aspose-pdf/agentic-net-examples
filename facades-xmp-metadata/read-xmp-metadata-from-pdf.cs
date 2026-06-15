using System;
using System.IO;
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

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(pdfPath);

        // Retrieve the XMP metadata as XML bytes
        byte[] xmlBytes = xmp.GetXmpMetadata();

        // Load the XML into an XmlDocument for parsing
        XmlDocument xmlDoc = new XmlDocument();
        using (MemoryStream ms = new MemoryStream(xmlBytes))
        {
            xmlDoc.Load(ms);
        }

        // Set up namespace manager for common XMP namespaces
        XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
        ns.AddNamespace("dc",   "http://purl.org/dc/elements/1.1/");
        ns.AddNamespace("xmp",  "http://ns.adobe.com/xap/1.0/");
        ns.AddNamespace("pdf",  "http://ns.adobe.com/pdf/1.3/");
        ns.AddNamespace("rdf",  "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        // Example of a PDF/A extension namespace (adjust if needed)
        ns.AddNamespace("pdfa", "http://www.aiim.org/pdfa/ns/property#");

        // ----- Read specific schema properties -----

        // dc:creator is typically a sequence; get the first <rdf:li> value
        XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", ns);
        string creator = creatorNode?.InnerText ?? "N/A";

        // xmp:CreateDate holds the creation timestamp
        XmlNode createDateNode = xmlDoc.SelectSingleNode("//xmp:CreateDate", ns);
        string createDate = createDateNode?.InnerText ?? "N/A";

        // Example of a custom PDF/A property (e.g., pdfa:part)
        XmlNode pdfaPartNode = xmlDoc.SelectSingleNode("//pdfa:part", ns);
        string pdfaPart = pdfaPartNode?.InnerText ?? "N/A";

        // Output the extracted values
        Console.WriteLine($"Creator      : {creator}");
        Console.WriteLine($"CreateDate   : {createDate}");
        Console.WriteLine($"PDF/A Part   : {pdfaPart}");

        // Release resources held by the facade
        xmp.Close();
    }
}