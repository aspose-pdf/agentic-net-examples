using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportXmpMetadataToJson
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "xmp_metadata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (creation & loading)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the XMP metadata facade
            PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
            xmpFacade.BindPdf(pdfDoc);

            // Retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmpFacade.GetXmpMetadata();

            // Convert XML bytes to string
            string xmlContent = Encoding.UTF8.GetString(xmlBytes);

            // Parse XML
            XDocument xDoc = XDocument.Parse(xmlContent);

            // Convert XML to a dictionary structure suitable for JSON serialization
            var jsonObject = XmlElementToDictionary(xDoc.Root);

            // Serialize dictionary to JSON with indentation
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to output file
            File.WriteAllText(outputJsonPath, json, Encoding.UTF8);
        }

        Console.WriteLine($"XMP metadata exported to JSON: {outputJsonPath}");
    }

    // Recursively converts an XElement into a dictionary.
    // Handles nested elements; if multiple sibling elements share the same name,
    // they are stored as a List<object>.
    private static object XmlElementToDictionary(XElement element)
    {
        var dict = new Dictionary<string, object>();

        // Process attributes as separate entries prefixed with '@'
        foreach (var attr in element.Attributes())
        {
            dict[$"@{attr.Name.LocalName}"] = attr.Value;
        }

        // Group child elements by name to detect repeats
        var childGroups = new Dictionary<string, List<XElement>>();
        foreach (var child in element.Elements())
        {
            if (!childGroups.ContainsKey(child.Name.LocalName))
                childGroups[child.Name.LocalName] = new List<XElement>();
            childGroups[child.Name.LocalName].Add(child);
        }

        // Convert each group
        foreach (var kvp in childGroups)
        {
            string name = kvp.Key;
            List<XElement> elems = kvp.Value;

            if (elems.Count == 1)
            {
                // Single element – recurse or take value
                dict[name] = elems[0].HasElements ? XmlElementToDictionary(elems[0]) : elems[0].Value;
            }
            else
            {
                // Multiple elements with same name – create a list
                var list = new List<object>();
                foreach (var el in elems)
                {
                    list.Add(el.HasElements ? XmlElementToDictionary(el) : el.Value);
                }
                dict[name] = list;
            }
        }

        // If element has no children and no attributes, return its value directly
        if (dict.Count == 0 && !element.HasElements)
        {
            return element.Value;
        }

        return dict;
    }
}