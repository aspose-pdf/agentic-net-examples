using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, XMP packet and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string xmpPacketPath = "metadata.xmp";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpPacketPath))
        {
            Console.Error.WriteLine($"XMP packet not found: {xmpPacketPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdfPath);

        // Update standard PDF metadata entries via the Document.Metadata dictionary
        doc.Metadata["Title"]   = "Merged Document Title";
        doc.Metadata["Author"]  = "John Doe";
        doc.Metadata["Subject"] = "Combined metadata example";
        doc.Metadata["Keywords"] = "Aspose, PDF, XMP, metadata";

        // ---------------------------------------------------------------------
        // Merge XMP metadata from the external packet.
        // Aspose.Pdf does not expose a SetXmpMetadata method; instead XMP data is
        // accessed through the Document.Metadata property.  We load the XMP XML,
        // register any namespaces that are required and copy the individual
        // properties into the Metadata dictionary.  The metadata is automatically
        // written back when the document is saved.
        // ---------------------------------------------------------------------
        byte[] xmpBytes = File.ReadAllBytes(xmpPacketPath);
        XDocument xmpDoc = XDocument.Parse(System.Text.Encoding.UTF8.GetString(xmpBytes));

        // Register common XMP namespaces (add more if your packet uses others)
        doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");
        doc.Metadata.RegisterNamespaceUri("dc",  "http://purl.org/dc/elements/1.1/");
        doc.Metadata.RegisterNamespaceUri("pdf", "http://ns.adobe.com/pdf/1.3/");
        doc.Metadata.RegisterNamespaceUri("xmpMM", "http://ns.adobe.com/xap/1.0/mm/");

        // The XMP packet follows the RDF structure.  We locate the first
        // <rdf:Description> element and copy each child element as a metadata
        // entry using its qualified name (prefix:localName).
        XNamespace rdfNs = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        var description = xmpDoc.Root?.Element(rdfNs + "Description");
        if (description != null)
        {
            foreach (var element in description.Elements())
            {
                // Build the key as "prefix:localName" (e.g., "dc:creator")
                string prefix = element.GetPrefixOfNamespace(element.Name.Namespace);
                if (string.IsNullOrEmpty(prefix))
                    continue; // skip elements without a registered prefix

                string key = $"{prefix}:{element.Name.LocalName}";
                string value = element.Value;
                // Overwrite or add the entry in the XMP metadata dictionary
                doc.Metadata[key] = value;
            }
        }

        // Save the document – XMP metadata is automatically merged with the PDF
        // info dictionary (PdfFileInfo) when the file is written.
        doc.Save(outputPdfPath);

        Console.WriteLine($"Metadata merged and saved to '{outputPdfPath}'.");
    }
}
