using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    // XML writer that will receive progress events
    private static XmlWriter _xmlWriter;

    // Progress handler that writes each event to the XML file
    private static void ProgressHandler(HtmlSaveOptions.ProgressEventHandlerInfo info)
    {
        // Write a single <ProgressEvent> element with relevant attributes
        _xmlWriter.WriteStartElement("ProgressEvent");
        _xmlWriter.WriteAttributeString("EventType", info.EventType.ToString());
        _xmlWriter.WriteAttributeString("Value", info.Value.ToString());
        _xmlWriter.WriteAttributeString("MaxValue", info.MaxValue.ToString());
        _xmlWriter.WriteAttributeString("DocumentId", info.DocumentId.ToString());
        _xmlWriter.WriteEndElement();
        _xmlWriter.Flush();
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string progressXmlPath = "progress.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Prepare XML writer (indent for readability)
        XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true };
        using (_xmlWriter = XmlWriter.Create(progressXmlPath, xmlSettings))
        {
            // Write root element
            _xmlWriter.WriteStartDocument();
            _xmlWriter.WriteStartElement("ProgressEvents");

            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Configure save options with the custom progress handler
                HtmlSaveOptions saveOptions = new HtmlSaveOptions();
                saveOptions.CustomProgressHandler = new HtmlSaveOptions.ConversionProgressEventHandler(ProgressHandler);

                // Save the document (conversion to HTML as an example)
                doc.Save(outputPdfPath, saveOptions);
            }

            // Close root element
            _xmlWriter.WriteEndElement(); // </ProgressEvents>
            _xmlWriter.WriteEndDocument();
        }

        Console.WriteLine($"Conversion completed. Progress saved to '{progressXmlPath}'.");
    }
}