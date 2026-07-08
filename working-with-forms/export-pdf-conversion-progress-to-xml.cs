using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // SaveOptions (e.g., HtmlSaveOptions) are now in the root Aspose.Pdf namespace

class Program
{
    // Path to the source PDF
    const string InputPdfPath = "input.pdf";

    // Path where the converted PDF will be saved (any format, here PDF)
    const string OutputPdfPath = "output.pdf";

    // Path to the XML file that will store progress information
    const string ProgressXmlPath = "progress.xml";

    static void Main()
    {
        // Ensure the input file exists
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {InputPdfPath}");
            return;
        }

        // Create an XmlWriter that will append progress entries
        XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, CloseOutput = true };
        using (XmlWriter xmlWriter = XmlWriter.Create(ProgressXmlPath, xmlSettings))
        {
            // Write the root element for progress tracking
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ProgressReport");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(InputPdfPath))
            {
                // HtmlSaveOptions is available directly under Aspose.Pdf namespace
                HtmlSaveOptions saveOptions = new HtmlSaveOptions();

                // Assign the custom progress handler
                saveOptions.CustomProgressHandler = new HtmlSaveOptions.ConversionProgressEventHandler(
                    (HtmlSaveOptions.ProgressEventHandlerInfo info) =>
                    {
                        // Write a single progress entry to the XML file
                        xmlWriter.WriteStartElement("Progress");
                        xmlWriter.WriteAttributeString("EventType", info.EventType.ToString());
                        xmlWriter.WriteAttributeString("Value", info.Value.ToString());
                        xmlWriter.WriteAttributeString("MaxValue", info.MaxValue.ToString());
                        // Guid is a non‑nullable value type; use a conditional check instead of the null‑conditional operator
                        string documentIdStr = info.DocumentId != Guid.Empty ? info.DocumentId.ToString() : string.Empty;
                        xmlWriter.WriteAttributeString("DocumentId", documentIdStr);
                        xmlWriter.WriteEndElement();

                        // Flush to ensure the entry is written promptly
                        xmlWriter.Flush();
                    });

                // Perform the save operation; progress events will trigger the handler above
                doc.Save(OutputPdfPath, saveOptions);
            }

            // Close the root element and the document
            xmlWriter.WriteEndElement(); // </ProgressReport>
            xmlWriter.WriteEndDocument();
        }

        Console.WriteLine($"Conversion completed. Progress saved to '{ProgressXmlPath}'.");
    }
}
