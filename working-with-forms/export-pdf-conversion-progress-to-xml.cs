using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string progressXml = "progress.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create an XML writer that will collect progress events
        using (XmlWriter writer = XmlWriter.Create(progressXml, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ProgressEvents");

            // Configure HTML save options with a custom progress handler
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CustomProgressHandler = new HtmlSaveOptions.ConversionProgressEventHandler(
                (HtmlSaveOptions.ProgressEventHandlerInfo info) =>
                {
                    // Write each progress event as an XML element
                    writer.WriteStartElement("Event");
                    writer.WriteAttributeString("Type", info.EventType.ToString());
                    writer.WriteAttributeString("Value", info.Value.ToString());
                    writer.WriteAttributeString("MaxValue", info.MaxValue.ToString());
                    writer.WriteEndElement();
                    writer.Flush(); // Ensure data is written promptly
                });

            // Load the PDF and save it as HTML while tracking progress
            using (Document doc = new Document(inputPdf))
            {
                doc.Save(outputHtml, saveOptions);
            }

            writer.WriteEndElement(); // </ProgressEvents>
            writer.WriteEndDocument();
        }

        Console.WriteLine($"Conversion completed. Progress saved to '{progressXml}'.");
    }
}