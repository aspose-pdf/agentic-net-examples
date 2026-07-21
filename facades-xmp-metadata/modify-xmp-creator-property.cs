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
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newCreator = "New Creator Name";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Bind the PDF to the XMP metadata facade
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Retrieve the existing XMP metadata as a byte array
            byte[] rawXmp = xmp.GetXmpMetadata();

            // Convert the byte array to a string (UTF‑8) and load it into an XmlDocument
            string xmpXml = Encoding.UTF8.GetString(rawXmp);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmpXml);

            // Prepare a namespace manager because XMP uses XML namespaces
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/"); // Dublin Core namespace

            // Locate the existing node (e.g., dc:creator) and modify its value
            XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator", ns);
            if (creatorNode != null)
            {
                creatorNode.InnerText = newCreator;
            }
            else
            {
                Console.WriteLine("Creator node not found; no changes applied.");
            }

            // Write the modified XML back into a memory stream
            using (MemoryStream updatedStream = new MemoryStream())
            {
                xmlDoc.Save(updatedStream);
                updatedStream.Position = 0; // reset stream position for reading

                // Set the updated XMP metadata on the document
                doc.SetXmpMetadata(updatedStream);

                // Save the PDF with the new XMP metadata
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}