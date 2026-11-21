using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class XmpNamespaceLister
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the XMP facade – this is the correct way to access XMP metadata.
            using (var xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(pdfPath);

                // Retrieve the raw XMP packet as a byte array.
                byte[] packet = xmp.GetXmpMetadata();

                if (packet == null || packet.Length == 0)
                {
                    Console.WriteLine("No XMP metadata found in the PDF.");
                    return;
                }

                // Convert the packet to a clean XML string.
                string xmpXml = Encoding.UTF8.GetString(packet)
                                            .Trim('\0', '\uFEFF', ' ', '\r', '\n');

                // Parse the XML.
                XDocument xdoc = XDocument.Parse(xmpXml);

                // Collect all namespace URIs declared in the document.
                var namespaces = new HashSet<string>();

                // Root element namespaces.
                foreach (XAttribute attr in xdoc.Root.Attributes())
                {
                    if (attr.IsNamespaceDeclaration)
                        namespaces.Add(attr.Value);
                }

                // Recursively check all elements for additional namespace declarations.
                foreach (XElement elem in xdoc.Descendants())
                {
                    foreach (XAttribute attr in elem.Attributes())
                    {
                        if (attr.IsNamespaceDeclaration)
                            namespaces.Add(attr.Value);
                    }
                }

                // Output the list of namespaces.
                Console.WriteLine("XMP namespaces present in the PDF:");
                foreach (string ns in namespaces.OrderBy(n => n))
                {
                    Console.WriteLine(ns);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
