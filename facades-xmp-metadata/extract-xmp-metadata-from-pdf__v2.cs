using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the PDF exists – if not, create a minimal sample PDF.
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);

            // Retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Load the XML into an XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmlBytes))
            {
                xmlDoc.Load(ms);
            }

            // Prepare namespace manager for common XMP namespaces
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            ns.AddNamespace("xmp", "http://ns.adobe.com/xap/1.0/");
            ns.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Example: read the creator (dc:creator)
            XmlNode? creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", ns);
            string creator = creatorNode?.InnerText ?? "(not set)";

            // Example: read the document title (dc:title)
            XmlNode? titleNode = xmlDoc.SelectSingleNode("//dc:title/rdf:Alt/rdf:li", ns);
            string title = titleNode?.InnerText ?? "(not set)";

            // Example: read a custom property (xmp:CreateDate)
            XmlNode? createDateNode = xmlDoc.SelectSingleNode("//xmp:CreateDate", ns);
            string createDate = createDateNode?.InnerText ?? "(not set)";

            // Output the extracted values
            Console.WriteLine($"Creator: {creator}");
            Console.WriteLine($"Title:   {title}");
            Console.WriteLine($"Created: {createDate}");
        }
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a very simple PDF with some basic metadata so that XMP exists.
        using (Document doc = new Document())
        {
            // Add a blank page.
            doc.Pages.Add();

            // Set basic document information – this information is also written to XMP.
            doc.Info.Title = "Sample PDF for XMP extraction";
            doc.Info.Author = "Aspose Sample Generator";
            doc.Info.Keywords = "XMP, Aspose.Pdf, Sample";
            doc.Info.Subject = "Demonstration of XMP metadata extraction";

            // Save the PDF to the specified path.
            doc.Save(path);
        }
    }
}
