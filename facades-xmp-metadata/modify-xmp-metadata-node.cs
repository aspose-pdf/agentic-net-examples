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
        const string xmpNodePath = "//dc:creator"; // XPath to the XMP node to modify
        const string newValue = "New Creator Name";

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

            // Retrieve the existing XMP metadata as a byte array
            byte[] xmpBytes = xmp.GetXmpMetadata();

            // Load the XMP XML into an XmlDocument for manipulation
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmpBytes))
            {
                xmlDoc.Load(ms);
            }

            // Register the Dublin Core namespace (dc) if not already present
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

            // Locate the target node using XPath
            XmlNode targetNode = xmlDoc.SelectSingleNode(xmpNodePath, nsMgr);
            if (targetNode != null)
            {
                // Update the node's inner text with the new value
                targetNode.InnerText = newValue;
            }
            else
            {
                Console.Error.WriteLine("Specified XMP node not found. No changes applied.");
            }

            // Save the modified XML back into a memory stream
            using (MemoryStream updatedStream = new MemoryStream())
            {
                xmlDoc.Save(updatedStream);
                updatedStream.Position = 0; // Reset stream position for reading

                // Apply the updated XMP metadata to the PDF document
                doc.SetXmpMetadata(updatedStream);
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}