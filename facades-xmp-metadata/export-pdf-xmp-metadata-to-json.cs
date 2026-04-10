using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf.Facades;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "xmp_metadata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the XMP metadata facade and bind the PDF file
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string
            string xmlString = Encoding.UTF8.GetString(xmlBytes);

            // Load the XML string into an XDocument for processing
            XDocument xdoc = XDocument.Parse(xmlString);

            // Convert the XML document to a formatted JSON string
            string json = JsonConvert.SerializeXNode(xdoc, Formatting.Indented, true);

            // Write the JSON output to a file
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"XMP metadata exported to JSON: {outputJson}");
        }
    }
}