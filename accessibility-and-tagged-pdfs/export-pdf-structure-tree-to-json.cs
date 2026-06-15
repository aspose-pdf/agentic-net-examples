using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "structure.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            JsonStructureNode jsonRoot = BuildJsonNode(root);
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonRoot, options);

            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Structure tree exported to '{outputJson}'.");
        }
    }

    // Recursively convert a StructureElement to a serializable node
    static JsonStructureNode BuildJsonNode(StructureElement element)
    {
        JsonStructureNode node = new JsonStructureNode {
            Type = element.GetType().Name,
            ActualText = element.ActualText,
            AlternativeText = element.AlternativeText,
            Language = element.Language,
            Children = new List<JsonStructureNode>()
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                node.Children.Add(BuildJsonNode(se));
        }

        return node;
    }

    // Simple POCO for JSON serialization
    class JsonStructureNode
    {
        public string Type { get; set; }
        public string ActualText { get; set; }
        public string AlternativeText { get; set; }
        public string Language { get; set; }
        public List<JsonStructureNode> Children { get; set; }
    }
}