using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;                     // Core PDF API (required for SetXmpMetadata)
using Aspose.Pdf.Facades;            // Facade API for XMP handling

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the existing XMP metadata using the Facade class.
        // ------------------------------------------------------------
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);                       // Bind the PDF file
        byte[] rawXmp = xmp.GetXmpMetadata();        // Retrieve XMP as XML bytes
        string xmlString = Encoding.UTF8.GetString(rawXmp);

        // ------------------------------------------------------------
        // 2. Modify the desired XML node (example: dc:creator).
        // ------------------------------------------------------------
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlString);

        // Namespace manager for common XMP namespaces
        XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
        ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

        // Locate the <dc:creator> element and change its text.
        XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator", ns);
        if (creatorNode != null)
        {
            creatorNode.InnerText = "New Creator Name";
        }
        else
        {
            // If the node does not exist, create it under the rdf:Description element.
            XmlNode descriptionNode = xmlDoc.SelectSingleNode("//rdf:Description", ns);
            if (descriptionNode != null)
            {
                XmlElement newCreator = xmlDoc.CreateElement("dc", "creator", ns.LookupNamespace("dc"));
                newCreator.InnerText = "New Creator Name";
                descriptionNode.AppendChild(newCreator);
            }
        }

        // ------------------------------------------------------------
        // 3. Write the modified XML back into the PDF.
        // ------------------------------------------------------------
        using (MemoryStream modifiedXmpStream = new MemoryStream())
        {
            // Save the edited XML into the memory stream.
            xmlDoc.Save(modifiedXmpStream);
            modifiedXmpStream.Position = 0; // Reset for reading.

            // Open the PDF with the core Document class (wrapped in using per lifecycle rule).
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Replace the XMP metadata with the edited stream.
                pdfDoc.SetXmpMetadata(modifiedXmpStream);

                // Save the updated PDF.
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}