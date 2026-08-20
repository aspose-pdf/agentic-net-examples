using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newCreator = "New Author";

        // Ensure input PDF exists (self‑contained example)
        if (!File.Exists(inputPdf))
        {
            using var seed = new Document();
            seed.Pages.Add();
            seed.Save(inputPdf);
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Bind XMP metadata facade to the document
            var xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Retrieve existing XMP metadata as XML string
            byte[] rawMetadata = xmp.GetXmpMetadata();
            string xml = Encoding.UTF8.GetString(rawMetadata);

            // Load XML into XmlDocument for manipulation
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            // Prepare namespace manager (XMP uses Dublin Core namespace for creator)
            var nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Locate the dc:creator node and modify its value
            XmlNode? creatorNode = xmlDoc.SelectSingleNode("//dc:creator", nsMgr);
            if (creatorNode != null)
            {
                creatorNode.InnerText = newCreator;
            }
            else
            {
                // If the node does not exist, create it under the first rdf:Description element
                XmlNode? descriptionNode = xmlDoc.SelectSingleNode("//rdf:Description", nsMgr);
                if (descriptionNode != null)
                {
                    string? dcNs = nsMgr.LookupNamespace("dc");
                    // dcNs is guaranteed because we added it above
                    var newCreatorElem = xmlDoc.CreateElement("dc", "creator", dcNs!);
                    newCreatorElem.InnerText = newCreator;
                    descriptionNode.AppendChild(newCreatorElem);
                }
            }

            // Save the modified XML back into a memory stream
            using var ms = new MemoryStream();
            xmlDoc.Save(ms);
            ms.Position = 0;

            // Write the updated XMP metadata back to the PDF
            doc.SetXmpMetadata(ms);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}
