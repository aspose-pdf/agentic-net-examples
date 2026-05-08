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
        const string targetXPath = "//dc:creator";          // XMP node to modify
        const string newValue   = "New Creator Name";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Bind the PDF to a PdfXmpMetadata facade
            PdfXmpMetadata xmpMeta = new PdfXmpMetadata();
            xmpMeta.BindPdf(pdfDoc);

            // Retrieve current XMP metadata as XML bytes
            byte[] xmpBytes = xmpMeta.GetXmpMetadata();
            string xmpXml   = Encoding.UTF8.GetString(xmpBytes);

            // Load XML into XmlDocument for manipulation
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmpXml);

            // Prepare namespace manager (XMP commonly uses the "dc" prefix)
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

            // Locate the target node and modify its text
            XmlNode node = xmlDoc.SelectSingleNode(targetXPath, nsMgr);
            if (node != null)
            {
                node.InnerText = newValue;
            }
            else
            {
                Console.Error.WriteLine($"XPath '{targetXPath}' not found in XMP metadata.");
                return;
            }

            // Convert the modified XML back to a byte array
            byte[] updatedXmpBytes = Encoding.UTF8.GetBytes(xmlDoc.OuterXml);

            // Apply the updated XMP metadata to the PDF document
            using (MemoryStream ms = new MemoryStream(updatedXmpBytes))
            {
                pdfDoc.SetXmpMetadata(ms);
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"XMP property updated and saved to '{outputPdf}'.");
    }
}