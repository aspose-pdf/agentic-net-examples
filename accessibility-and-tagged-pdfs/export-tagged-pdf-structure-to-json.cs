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
        const string inputPath = "input.pdf";
        const string jsonPath  = "structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Convert the structure tree to a serializable POCO hierarchy
            ElementNode jsonRoot = BuildElementNode(root);

            // Serialize hierarchy to formatted JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonRoot, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Tagged structure exported to '{jsonPath}'.");
    }

    // Recursively builds a plain object representing a structure element
    static ElementNode BuildElementNode(Element element)
    {
        ElementNode node = new ElementNode {
            Type            = element.GetType().Name,
            Text            = (element as StructureElement)?.ActualText,
            AlternativeText = (element as StructureElement)?.AlternativeText,
            Language        = (element as StructureElement)?.Language,
            Children        = new List<ElementNode>()
        };

        // Process child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            node.Children.Add(BuildElementNode(child));
        }

        return node;
    }

    // POCO used for JSON serialization
    class ElementNode
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string AlternativeText { get; set; }
        public string Language { get; set; }
        public List<ElementNode> Children { get; set; }
    }
}