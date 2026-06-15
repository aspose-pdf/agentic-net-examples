using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_custom_metadata.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add custom key/value pairs to the DocumentInfo dictionary.
            //    DocumentInfo inherits from Dictionary<string,string>, so Add works.
            // -----------------------------------------------------------------
            DocumentInfo info = doc.Info;               // Access the existing info object
            info.Add("ProcessingId", Guid.NewGuid().ToString()); // Example custom key
            info.Add("SourceSystem", "MyApp");          // Another custom entry

            // -----------------------------------------------------------------
            // 2. Create custom XMP metadata as XML and embed it into the PDF.
            //    The XML is written to a MemoryStream and passed to SetXmpMetadata.
            // -----------------------------------------------------------------
            XmlDocument xmpXml = new XmlDocument();

            // Create the root element for XMP metadata (any namespace can be used)
            XmlElement rdf = xmpXml.CreateElement("xmpmeta", "http://ns.adobe.com/xap/1.0/");
            xmpXml.AppendChild(rdf);

            // Example custom element
            XmlElement custom = xmpXml.CreateElement("myCustomData", "http://example.com/custom/");
            custom.SetAttribute("ProcessingDate", DateTime.UtcNow.ToString("o"));
            custom.InnerText = "Ready for downstream processing";
            rdf.AppendChild(custom);

            // Write the XML into a stream (UTF-8 encoding)
            using (MemoryStream xmpStream = new MemoryStream())
            {
                xmpXml.Save(xmpStream);
                xmpStream.Position = 0; // Reset stream position before reading

                // Embed the XMP metadata into the document
                doc.SetXmpMetadata(xmpStream);
            }

            // -----------------------------------------------------------------
            // 3. Save the modified PDF. No SaveOptions are required for PDF output.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}