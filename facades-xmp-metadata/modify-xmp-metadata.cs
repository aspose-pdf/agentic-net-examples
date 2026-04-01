using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Load existing XMP metadata
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            byte[] xmpBytes = xmp.GetXmpMetadata();
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmpBytes))
            {
                xmlDoc.Load(ms);
            }

            // Prepare namespace manager for common XMP namespaces
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            nsMgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

            // Modify the dc:creator element (if it exists)
            XmlNode creatorNode = xmlDoc.SelectSingleNode("//dc:creator/rdf:Seq/rdf:li", nsMgr);
            if (creatorNode != null)
            {
                creatorNode.InnerText = "New Creator Name";
                Console.WriteLine("Updated dc:creator.");
            }
            else
            {
                Console.WriteLine("Creator node not found; adding a new one.");
                XmlNode rdfDescription = xmlDoc.SelectSingleNode("//rdf:Description", nsMgr);
                if (rdfDescription != null)
                {
                    XmlElement newCreator = xmlDoc.CreateElement("dc", "creator", nsMgr.LookupNamespace("dc"));
                    XmlElement seq = xmlDoc.CreateElement("rdf", "Seq", nsMgr.LookupNamespace("rdf"));
                    XmlElement li = xmlDoc.CreateElement("rdf", "li", nsMgr.LookupNamespace("rdf"));
                    li.InnerText = "New Creator Name";
                    seq.AppendChild(li);
                    newCreator.AppendChild(seq);
                    rdfDescription.AppendChild(newCreator);
                }
            }

            // Write the modified XMP back into the PDF
            using (MemoryStream outMs = new MemoryStream())
            {
                xmlDoc.Save(outMs);
                outMs.Position = 0;
                doc.SetXmpMetadata(outMs);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}