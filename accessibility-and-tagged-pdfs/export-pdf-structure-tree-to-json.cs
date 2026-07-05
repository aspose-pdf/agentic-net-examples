using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // Represents a node in the JSON structure tree.
    private class JsonStructureNode
    {
        public string Type { get; set; } = string.Empty;
        public string? ActualText { get; set; }
        public string? AlternativeText { get; set; }
        public List<JsonStructureNode> Children { get; set; } = new List<JsonStructureNode>();
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

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Access the tagged content (logical structure) of the document.
            ITaggedContent tagged = doc.TaggedContent;

            // The root element of the structure tree.
            StructureElement root = tagged.RootElement;

            // Convert the structure tree to a serializable object.
            JsonStructureNode jsonRoot = ConvertElement(root);

            // Serialize to JSON with optional indentation.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(jsonRoot, jsonOptions);

            // Write the JSON to the output file.
            File.WriteAllText(outputJson, jsonString);
        }

        Console.WriteLine($"Structure tree exported to '{outputJson}'.");
    }

    // Recursively converts a StructureElement into a JsonStructureNode.
    private static JsonStructureNode ConvertElement(StructureElement element)
    {
        JsonStructureNode node = new JsonStructureNode {
            Type = element.GetType().Name,
            ActualText = element.ActualText,
            AlternativeText = element.AlternativeText
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                node.Children.Add(ConvertElement(childStructure));
            }
        }

        return node;
    }
}