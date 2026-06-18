using System;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "xmp_metadata.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load XMP metadata as XML bytes using PdfXmpMetadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Parse XML
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(xmlBytes))
            {
                xmlDoc.Load(ms);
            }

            // Convert XML DOM to a generic object graph suitable for JSON serialization
            object jsonObject = ConvertXmlNode(xmlDoc.DocumentElement);

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"XMP metadata exported to JSON: {outputJson}");
        }
    }

    // Recursively converts an XmlNode into a dictionary / value structure
    private static object ConvertXmlNode(XmlNode node)
    {
        // If the node has no child elements, return its text content
        if (!node.HasChildNodes)
            return node.InnerText;

        // If the node has a single text child, treat it as a simple value
        if (node.ChildNodes.Count == 1 && node.FirstChild.NodeType == XmlNodeType.Text)
            return node.InnerText;

        var dict = new Dictionary<string, object>();

        // Process child elements
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType != XmlNodeType.Element)
                continue;

            object childValue = ConvertXmlNode(child);

            if (dict.ContainsKey(child.Name))
            {
                // Multiple elements with same name become a list
                if (dict[child.Name] is List<object> list)
                {
                    list.Add(childValue);
                }
                else
                {
                    var newList = new List<object> { dict[child.Name], childValue };
                    dict[child.Name] = newList;
                }
            }
            else
            {
                dict[child.Name] = childValue;
            }
        }

        // Include attributes, if any
        if (node.Attributes != null && node.Attributes.Count > 0)
        {
            var attrDict = new Dictionary<string, string>();
            foreach (XmlAttribute attr in node.Attributes)
                attrDict[attr.Name] = attr.Value;
            dict["@attributes"] = attrDict;
        }

        return dict;
    }
}