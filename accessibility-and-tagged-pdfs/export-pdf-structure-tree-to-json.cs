using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // POCO class that represents a node in the structure tree for JSON serialization
    class JsonStructureNode
    {
        public string Type { get; set; }
        public string AlternativeText { get; set; }
        public string ActualText { get; set; }
        public string Language { get; set; }
        public List<JsonStructureNode> Children { get; set; } = new List<JsonStructureNode>();
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "structure.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using rule‑based lifecycle)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the structure tree root element via ITaggedContent
            ITaggedContent taggedContent = doc.TaggedContent;
            Aspose.Pdf.LogicalStructure.StructTreeRootElement rootElement = taggedContent.StructTreeRootElement;

            // Convert the structure tree to a serializable object graph
            JsonStructureNode rootNode = ConvertElementToNode(rootElement);

            // Serialize to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNode, jsonOptions);

            // Write JSON to file (non‑PDF output, so use standard I/O)
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Structure tree exported to '{outputJsonPath}'.");
    }

    // Recursively converts an Aspose.Pdf logical structure Element into a JsonStructureNode
    static JsonStructureNode ConvertElementToNode(Element element)
    {
        JsonStructureNode node = new JsonStructureNode {
            Type = element.GetType().Name
        };

        // Only StructureElement (and its derived types) expose the text‑related properties
        if (element is StructureElement se)
        {
            node.AlternativeText = se.AlternativeText;
            node.ActualText      = se.ActualText;
            node.Language        = se.Language;
        }

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            node.Children.Add(ConvertElementToNode(child));
        }

        return node;
    }
}