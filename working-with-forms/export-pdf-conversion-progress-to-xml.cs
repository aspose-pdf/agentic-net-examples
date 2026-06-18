using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string progressXml = "progress.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialise the XML file that will store progress events
        XDocument initDoc = new XDocument(new XElement("ProgressEvents"));
        initDoc.Save(progressXml);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Configure HTML save options and attach a custom progress handler
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CustomProgressHandler = new HtmlSaveOptions.ConversionProgressEventHandler(
                (info) => RecordProgress(info, progressXml));

            // Save the document (any format that supports progress events can be used)
            doc.Save(outputHtml, saveOptions);
        }

        Console.WriteLine($"Conversion finished. Progress events saved to '{progressXml}'.");
    }

    // Called for each progress event; appends the event data to the XML file
    static void RecordProgress(HtmlSaveOptions.ProgressEventHandlerInfo info, string xmlPath)
    {
        // Load existing XML, add a new <Event> element, and save back
        XDocument doc = XDocument.Load(xmlPath);
        doc.Root.Add(new XElement("Event",
            new XAttribute("Type", info.EventType),
            new XAttribute("Value", info.Value),
            new XAttribute("MaxValue", info.MaxValue)));
        doc.Save(xmlPath);
    }
}