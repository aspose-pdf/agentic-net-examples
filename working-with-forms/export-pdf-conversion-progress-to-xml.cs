using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    // XML document that will collect progress events
    private static readonly XmlDocument progressXml = new XmlDocument();

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.html";
        const string progressXmlPath = "progress.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialise the XML document with a root element
        XmlElement root = progressXml.CreateElement("ProgressEvents");
        progressXml.AppendChild(root);

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Configure HTML save options with a custom progress handler
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Assign the handler that records progress into the XML document
                CustomProgressHandler = new HtmlSaveOptions.ConversionProgressEventHandler(RecordProgress)
            };

            // Save the document; the progress handler will be invoked during the conversion
            doc.Save(outputPdfPath, htmlOpts);
        }

        // After conversion, write the collected progress information to an XML file
        progressXml.Save(progressXmlPath);
        Console.WriteLine($"Conversion completed. Progress data saved to '{progressXmlPath}'.");
    }

    // Handler that receives progress events and appends them to the XML document
    private static void RecordProgress(HtmlSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // Create an <Event> element with relevant attributes
        XmlElement eventElem = progressXml.CreateElement("Event");
        eventElem.SetAttribute("Timestamp", DateTime.Now.ToString("o"));
        eventElem.SetAttribute("EventType", eventInfo.EventType.ToString());
        eventElem.SetAttribute("Value", eventInfo.Value.ToString());
        eventElem.SetAttribute("MaxValue", eventInfo.MaxValue.ToString());

        // Append the event to the root <ProgressEvents> element
        progressXml.DocumentElement?.AppendChild(eventElem);
    }
}