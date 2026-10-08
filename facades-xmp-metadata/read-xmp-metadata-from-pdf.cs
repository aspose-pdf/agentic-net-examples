using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

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

        // Load the PDF document – this gives access to the Metadata dictionary where XMP is stored
        Document doc = new Document(pdfPath);

        // XMP metadata is stored under the key "XmpMetadata" in the Metadata dictionary.
        // The dictionary returns an Aspose.Pdf.XmpValue, which must be converted to a string.
        string xmpXml = null;
        if (doc.Metadata != null && doc.Metadata.TryGetValue("XmpMetadata", out var xmpValue) && xmpValue != null)
        {
            xmpXml = xmpValue.ToString(); // explicit conversion from XmpValue to string
        }

        if (string.IsNullOrEmpty(xmpXml))
        {
            Console.WriteLine("No XMP metadata found in the PDF.");
            return;
        }

        // Parse the XMP XML string
        XDocument xdoc = XDocument.Parse(xmpXml);
        var root = xdoc.Root;
        if (root == null)
        {
            Console.WriteLine("Invalid XMP XML: missing root element.");
            return;
        }

        // Common XMP namespaces (adjust as needed for your schema)
        XNamespace dc  = "http://purl.org/dc/elements/1.1/";
        XNamespace pdf = "http://ns.adobe.com/pdf/1.3/";
        XNamespace xmp = "http://ns.adobe.com/xap/1.0/";

        // Extract specific property values; use null‑coalescing to handle missing elements
        string title      = root.Element(dc + "title")?.Value      ?? "(no title)";
        string creator    = root.Element(dc + "creator")?.Value    ?? "(no creator)";
        string pdfVersion = root.Element(pdf + "PDFVersion")?.Value ?? "(no PDF version)";
        string modifyDate = root.Element(xmp + "ModifyDate")?.Value ?? "(no modify date)";

        // Output the extracted values
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Creator: {creator}");
        Console.WriteLine($"PDF Version: {pdfVersion}");
        Console.WriteLine($"Modify Date: {modifyDate}");
    }
}