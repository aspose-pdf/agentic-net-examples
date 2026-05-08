using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputJson = "metadata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the XMP metadata facade and bind it to the PDF
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc); // Bind using the loaded Document instance

            // Retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Parse the XML bytes into an XDocument
            XDocument xdoc = XDocument.Parse(System.Text.Encoding.UTF8.GetString(xmlBytes));

            // Convert the XDocument to a formatted JSON string
            string json = JsonConvert.SerializeXNode(xdoc, Formatting.Indented);

            // Write the JSON output to a file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"XMP metadata exported to JSON: {outputJson}");
    }
}