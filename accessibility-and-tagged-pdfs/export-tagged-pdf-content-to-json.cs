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
        const string outputPath = "tagged_content.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF contains a tagged structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to export.");
                return;
            }

            // Access tagged content via the proper API
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Build a hierarchical model representing the structure tree
            Node jsonRoot = BuildNode(root);

            // Serialize the model to formatted JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonRoot, jsonOptions);
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Tagged content exported to '{outputPath}'.");
        }
    }

    // Simple POCO for JSON serialization
    private class Node
    {
        public string Type { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string AlternativeText { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public List<Node> Children { get; set; } = new List<Node>();
    }

    // Recursively convert a StructureElement into a Node
    private static Node BuildNode(StructureElement element)
    {
        Node node = new Node
        {
            Type = element.GetType().Name,
            Text = element.ActualText ?? string.Empty,
            AlternativeText = element.AlternativeText ?? string.Empty,
            Language = element.Language ?? string.Empty
        };

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                node.Children.Add(BuildNode(se));
            }
        }

        return node;
    }
}
