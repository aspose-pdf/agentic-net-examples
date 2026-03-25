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
        const string outputPath = "tagged_structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("Document does not contain tagged content.");
                return;
            }

            StructureElement root = tagged.RootElement;
            Node rootNode = BuildNode(root);
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Tagged structure saved to '{outputPath}'.");
        }
    }

    static Node BuildNode(StructureElement element)
    {
        var node = new Node
        {
            Type = element.GetType().Name,
            Text = element.ActualText,
            AlternativeText = element.AlternativeText,
            Children = new List<Node>()
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                node.Children.Add(BuildNode(se));
            }
        }

        return node;
    }

    class Node
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string AlternativeText { get; set; }
        public List<Node> Children { get; set; }
    }
}