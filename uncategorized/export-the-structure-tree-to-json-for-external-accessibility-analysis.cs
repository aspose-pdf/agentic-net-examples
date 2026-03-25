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
        const string jsonPath = "structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            var tree = BuildNode(root);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tree, options);
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Structure tree exported to '{jsonPath}'.");
    }

    static Node BuildNode(StructureElement element)
    {
        var node = new Node
        {
            Type = element.GetType().Name,
            AlternativeText = element.AlternativeText,
            ActualText = element.ActualText,
            Language = element.Language,
            Children = new List<Node>()
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                node.Children.Add(BuildNode(se));
        }

        return node;
    }

    class Node
    {
        public string Type { get; set; }
        public string AlternativeText { get; set; }
        public string ActualText { get; set; }
        public string Language { get; set; }
        public List<Node> Children { get; set; }
    }
}