using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // DTO that mirrors the logical structure element for JSON serialization
    private class StructureNode
    {
        public string ElementType { get; set; }
        public string ActualText { get; set; }
        public string AlternativeText { get; set; }
        public string Language { get; set; }
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "structure.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content; if the document is not tagged, the root will be empty
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement rootElement = tagged.RootElement;

            // Convert the structure tree to a serializable object graph
            StructureNode rootNode = ConvertElement(rootElement);

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Structure tree exported to '{outputJson}'.");
    }

    // Recursively converts a StructureElement (or any Element) into a StructureNode DTO
    private static StructureNode ConvertElement(Element element)
    {
        // Only process StructureElement instances; other element types are ignored
        if (element is not StructureElement structElem)
            return null;

        StructureNode node = new StructureNode {
            ElementType = structElem.GetType().Name,
            ActualText = structElem.ActualText,
            AlternativeText = structElem.AlternativeText,
            Language = structElem.Language
        };

        // Iterate over child elements
        foreach (Element child in structElem.ChildElements)
        {
            StructureNode childNode = ConvertElement(child);
            if (childNode != null)
                node.Children.Add(childNode);
        }

        return node;
    }
}