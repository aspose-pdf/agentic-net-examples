using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load XMP metadata using the Facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            byte[] rawData = xmp.GetXmpMetadata();

            if (rawData == null || rawData.Length == 0)
            {
                Console.WriteLine("No XMP metadata found in the PDF.");
                return;
            }

            // Parse XML
            XmlDocument doc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(rawData))
            {
                doc.Load(ms);
            }

            // Collect unique namespace URIs
            HashSet<string> namespaces = new HashSet<string>();
            CollectNamespaces(doc.DocumentElement, namespaces);

            Console.WriteLine("XMP Namespaces present in the PDF:");
            foreach (string ns in namespaces)
            {
                Console.WriteLine(ns);
            }
        }
    }

    // Recursively walk the XML tree and gather xmlns declarations
    static void CollectNamespaces(XmlNode node, HashSet<string> set)
    {
        if (node.Attributes != null)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Prefix == "xmlns")
                {
                    set.Add(attr.Value);
                }
                else if (attr.Name == "xmlns") // default namespace
                {
                    set.Add(attr.Value);
                }
            }
        }

        foreach (XmlNode child in node.ChildNodes)
        {
            CollectNamespaces(child, set);
        }
    }
}