using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class StructureNode
{
    public string Type { get; set; }
    public string Text { get; set; }
    public string AlternativeText { get; set; }
    public string Language { get; set; }
    public List<StructureNode> Children { get; set; } = new List<StructureNode>();
}

class Program
{
    static StructureNode BuildNode(Element element)
    {
        StructureNode node = new StructureNode {
            Type = element.GetType().Name,
            Text = (element as StructureElement)?.ActualText,
            AlternativeText = (element as StructureElement)?.AlternativeText,
            Language = (element as StructureElement)?.Language
        };

        if (element is StructureElement se)
        {
            foreach (Element child in se.ChildElements)
            {
                node.Children.Add(BuildNode(child));
            }
        }

        return node;
    }

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
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The document does not contain a tagged structure.");
                return;
            }

            StructureNode rootNode = BuildNode(tagged.RootElement);
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Structure hierarchy saved to '{jsonPath}'.");
        }
    }
}