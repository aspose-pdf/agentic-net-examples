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
        const string jsonOutput = "xmp_metadata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Convert bytes to a UTF‑8 string
            string xmlString = Encoding.UTF8.GetString(xmlBytes);

            // Parse the XML
            XDocument xdoc = XDocument.Parse(xmlString);

            // Convert XML to JSON (indented for readability)
            string json = JsonConvert.SerializeXNode(xdoc, Formatting.Indented, true);

            // Write JSON to the output file
            File.WriteAllText(jsonOutput, json);
        }

        Console.WriteLine($"XMP metadata exported to JSON: {jsonOutput}");
    }
}