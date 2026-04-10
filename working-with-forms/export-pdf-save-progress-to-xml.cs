using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // HtmlSaveOptions and other PDF classes are in this namespace

class ProgressExportExample
{
    // Paths for input PDF, output PDF, and the XML file that will store progress information
    private const string InputPdfPath = "input.pdf";
    private const string OutputPdfPath = "output.pdf";
    private const string ProgressXmlPath = "progress.xml";

    static void Main()
    {
        // Verify the input file exists
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {InputPdfPath}");
            return;
        }

        // Create an XmlWriter that will write progress events to an XML file
        using (XmlWriter xmlWriter = XmlWriter.Create(ProgressXmlPath, new XmlWriterSettings { Indent = true }))
        {
            // Write the root element
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ProgressEvents");

            // Load the PDF document
            using (Document doc = new Document(InputPdfPath))
            {
                // Configure save options – HtmlSaveOptions is used here only to demonstrate progress handling
                HtmlSaveOptions saveOptions = new HtmlSaveOptions();

                // Assign the custom progress handler (lambda matches the required delegate signature)
                saveOptions.CustomProgressHandler = info => WriteProgressEvent(xmlWriter, info);

                // Perform the save operation; progress events will be emitted during this call
                doc.Save(OutputPdfPath, saveOptions);
            }

            // Close the root element
            xmlWriter.WriteEndElement(); // </ProgressEvents>
            xmlWriter.WriteEndDocument();
        }

        Console.WriteLine($"Document saved to '{OutputPdfPath}'.");
        Console.WriteLine($"Progress information exported to '{ProgressXmlPath}'.");
    }

    // Writes a single progress event as an XML element
    private static void WriteProgressEvent(XmlWriter writer, HtmlSaveOptions.ProgressEventHandlerInfo info)
    {
        writer.WriteStartElement("Event");
        writer.WriteAttributeString("Timestamp", DateTime.Now.ToString("o"));
        writer.WriteAttributeString("EventType", info.EventType.ToString());
        writer.WriteAttributeString("Value", info.Value.ToString());
        writer.WriteAttributeString("MaxValue", info.MaxValue.ToString());
        // Guid is a non‑nullable struct; use a direct ToString() call (or guard against Guid.Empty if desired)
        writer.WriteAttributeString("DocumentId", info.DocumentId != Guid.Empty ? info.DocumentId.ToString() : string.Empty);
        writer.WriteEndElement(); // </Event>
    }
}
