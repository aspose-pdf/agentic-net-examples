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
            // Add custom key‑value pairs to the DocumentInfo dictionary
            doc.Info.Add("CustomKey1", "CustomValue1");
            doc.Info.Add("CustomKey2", "CustomValue2");

            // Build custom XMP metadata as XML
            XmlDocument xmpDoc = new XmlDocument();

            // Root element for XMP metadata
            XmlElement xmpMeta = xmpDoc.CreateElement("x:xmpmeta", "adobe:ns:meta/");
            xmpDoc.AppendChild(xmpMeta);

            // Description element with a custom namespace
            XmlElement description = xmpDoc.CreateElement("rdf:Description", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            description.SetAttribute("xmlns:custom", "http://example.com/custom");
            description.SetAttribute("custom:ProcessingInfo", "Downstream");
            xmpMeta.AppendChild(description);

            // Write the XML into a memory stream and set it as XMP metadata
            using (MemoryStream ms = new MemoryStream())
            {
                xmpDoc.Save(ms);
                ms.Position = 0; // Reset stream position before reading
                doc.SetXmpMetadata(ms);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}