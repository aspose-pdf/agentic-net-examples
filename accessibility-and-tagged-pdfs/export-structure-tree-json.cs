using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

namespace ExportStructureTree
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "structure.json";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            using (Document doc = new Document(inputPath))
            {
                ITaggedContent tagged = doc.TaggedContent;
                // If the document is not tagged, the RootElement will be null.
                if (tagged?.RootElement == null)
                {
                    Console.WriteLine("The PDF does not contain a tagged structure.");
                    return;
                }

                StructureElement root = tagged.RootElement;
                StructureNode jsonRoot = ConvertElement(root);

                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(jsonRoot, jsonOptions);
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Structure tree exported to '{outputPath}'.");
            }
        }

        // Recursively converts a logical structure element into a serializable node.
        private static StructureNode ConvertElement(Element element)
        {
            var structElem = element as StructureElement;
            var node = new StructureNode
            {
                Type = element.GetType().Name,
                AlternativeText = structElem?.AlternativeText,
                ActualText = structElem?.ActualText,
                Language = structElem?.Language,
                Children = new List<StructureNode>()
            };

            foreach (Element child in element.ChildElements)
            {
                node.Children.Add(ConvertElement(child));
            }

            return node;
        }
    }

    // Simple POCO that matches the JSON structure we want.
    public class StructureNode
    {
        public string Type { get; set; }
        public string AlternativeText { get; set; }
        public string ActualText { get; set; }
        public string Language { get; set; }
        public List<StructureNode> Children { get; set; }
    }
}