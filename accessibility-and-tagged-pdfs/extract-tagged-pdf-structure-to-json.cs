using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // POCO representing a node in the tagged structure tree
    private class TaggedNode
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string AlternativeText { get; set; }
        public List<TaggedNode> Children { get; set; } = new List<TaggedNode>();
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "tagged_structure.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Access tagged content (no need to set language/title for extraction)
                ITaggedContent tagged = pdfDoc.TaggedContent;

                // Root element of the logical structure
                StructureElement root = tagged.RootElement;

                // Build a hierarchical representation
                TaggedNode rootNode = BuildNode(root);

                // Serialize hierarchy to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(rootNode, jsonOptions);
                File.WriteAllText(outputJsonPath, json);

                Console.WriteLine($"Tagged structure exported to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively converts a StructureElement into a TaggedNode
    private static TaggedNode BuildNode(StructureElement element)
    {
        var node = new TaggedNode
        {
            Type = element.GetType().Name,
            Text = element.ActualText,
            AlternativeText = element.AlternativeText
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                node.Children.Add(BuildNode(childStruct));
            }
        }

        return node;
    }
}