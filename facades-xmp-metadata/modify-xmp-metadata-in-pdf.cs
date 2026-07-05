using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Bind the PDF to the XMP metadata facade
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Load the XML into an XmlDocument for manipulation
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                xmlDoc.Load(xmlStream);
            }

            // Example modification: change the dc:creator element value
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

            XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator", nsMgr);
            if (creatorNode != null)
            {
                creatorNode.InnerText = "New Creator";
            }

            // Write the modified XML back into a stream
            using (MemoryStream modifiedXml = new MemoryStream())
            {
                xmlDoc.Save(modifiedXml);
                modifiedXml.Position = 0;

                // Update the PDF with the modified XMP metadata
                doc.SetXmpMetadata(modifiedXml);
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}