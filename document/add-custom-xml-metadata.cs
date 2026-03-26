using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a custom entry to the document information dictionary (DocumentInfo)
            // The value can be any string, including XML markup for downstream processing
            doc.Info.Add("CustomData", "<mydata><value>123</value></mydata>");

            // Create custom XMP metadata as an XML document
            XmlDocument xmp = new XmlDocument();
            xmp.LoadXml(
                "<x:xmpmeta xmlns:x='adobe:ns:meta/'>" +
                "<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>" +
                "<rdf:Description rdf:about='' xmlns:custom='http://example.com/custom/'>" +
                "<custom:ProcessingInfo>Downstream</custom:ProcessingInfo>" +
                "</rdf:Description>" +
                "</rdf:RDF>" +
                "</x:xmpmeta>");

            // Write the XMP XML into a memory stream and attach it to the PDF
            using (MemoryStream ms = new MemoryStream())
            {
                xmp.Save(ms);
                ms.Position = 0;
                doc.SetXmpMetadata(ms);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom metadata saved to '{outputPath}'.");
    }
}