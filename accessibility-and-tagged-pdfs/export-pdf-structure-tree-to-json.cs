using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure; // Added for completeness when using dynamic

class Program
{
    // DTO representing a node in the PDF structure tree
    public class StructureNode
    {
        public string? ElementType { get; set; }
        public string? Text { get; set; }
        public string? AlternativeText { get; set; }
        public string? Language { get; set; }
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking TaggedContent
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged; no structure tree to export.");
                return;
            }

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the structure tree – use dynamic to avoid compile‑time dependency on StructureElement
            dynamic rootElement = taggedContent.RootElement;

            // Build a serializable representation of the tree
            StructureNode rootNode = BuildNode(rootElement);

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Structure tree exported to '{outputPath}'.");
        }
    }

    // Recursively converts a structure element (dynamic) into a StructureNode DTO
    private static StructureNode BuildNode(dynamic element)
    {
        StructureNode node = new StructureNode
        {
            // element.GetType().Name gives the concrete runtime type name (e.g., "DivElement", "SectElement", etc.)
            ElementType = element.GetType().Name,
            Text = element.ActualText,
            AlternativeText = element.AlternativeText,
            Language = element.Language
        };

        // Iterate over child elements using the ChildElements collection
        foreach (dynamic child in element.ChildElements)
        {
            // Only process children that are structure elements (they expose the same dynamic members)
            node.Children.Add(BuildNode(child));
        }

        return node;
    }
}
