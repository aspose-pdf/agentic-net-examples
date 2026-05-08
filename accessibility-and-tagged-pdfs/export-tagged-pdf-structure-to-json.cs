using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // POCO representing a node in the tagged structure hierarchy
    class StructureNode
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string AltText { get; set; }
        public string Language { get; set; }
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "tagged_structure.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // If the document has no tagged structure, create an empty JSON file
                if (tagged == null || tagged.RootElement == null)
                {
                    File.WriteAllText(outputJson, "{}");
                    Console.WriteLine("Document is not tagged. Empty JSON created.");
                    return;
                }

                // Build a hierarchical model from the root element
                StructureNode rootNode = BuildNode(tagged.RootElement);

                // Serialize the hierarchy to JSON with indentation
                string json = JsonSerializer.Serialize(rootNode, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write JSON to the output file
                File.WriteAllText(outputJson, json);
                Console.WriteLine($"Tagged structure exported to '{outputJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively converts a StructureElement into a StructureNode
    static StructureNode BuildNode(StructureElement element)
    {
        StructureNode node = new StructureNode {
            Type = element.GetType().Name,
            Text = element.ActualText,
            AltText = element.AlternativeText,
            Language = element.Language
        };

        // Iterate over child elements (if any) and process them recursively
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                node.Children.Add(BuildNode(childStructure));
            }
        }

        return node;
    }
}