using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    // Simple POCO that represents a structure element for JSON serialization
    private class StructureNode
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("actualText")]
        public string ActualText { get; set; }

        [JsonPropertyName("alternativeText")]
        public string AlternativeText { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("children")]
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
    }

    // Recursively converts an Aspose.Pdf logical structure Element into a serializable StructureNode
    private static StructureNode ConvertToNode(Element element)
    {
        StructureNode node = new StructureNode {
            Type = element.GetType().Name
        };

        // Most elements that carry textual information derive from StructureElement
        if (element is StructureElement se)
        {
            node.ActualText = se.ActualText;
            node.AlternativeText = se.AlternativeText;
            node.Language = se.Language;
        }

        // Process child elements recursively
        foreach (Element child in element.ChildElements)
        {
            node.Children.Add(ConvertToNode(child));
        }

        return node;
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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Obtain the root of the structure tree
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // Convert the entire structure tree to a serializable object
            StructureNode rootNode = ConvertToNode(structRoot);

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Structure tree exported to '{outputJson}'.");
    }
}