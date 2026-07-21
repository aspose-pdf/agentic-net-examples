using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, Element, StructTreeRootElement

class Program
{
    // Simple DTO that mirrors the logical structure for JSON serialization
    private class StructureNode
    {
        public string ElementType { get; set; }
        public string AlternativeText { get; set; }
        public string Language { get; set; }
        public string ActualText { get; set; }
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
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

        // Load the PDF document (using the standard lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access tagged content – the document may or may not be tagged.
            ITaggedContent taggedContent = doc.TaggedContent;
            if (taggedContent == null)
            {
                Console.Error.WriteLine("Document does not contain tagged content.");
                return;
            }

            // Get the root of the structure tree.
            StructTreeRootElement structRoot = taggedContent.StructTreeRootElement;

            // Convert the structure tree to a serializable object graph.
            StructureNode jsonRoot = ConvertElement(structRoot);

            // Serialize to JSON with optional indentation.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(jsonRoot, jsonOptions);

            // Write the JSON to a file.
            File.WriteAllText(outputJsonPath, jsonString);
            Console.WriteLine($"Structure tree exported to '{outputJsonPath}'.");
        }
    }

    // Recursively converts an Aspose.Pdf.LogicalStructure.Element into a StructureNode.
    private static StructureNode ConvertElement(Element element)
    {
        StructureNode node = new StructureNode {
            ElementType = element.GetType().Name,
            AlternativeText = (element as StructureElement)?.AlternativeText,
            Language = (element as StructureElement)?.Language,
            ActualText = (element as StructureElement)?.ActualText
        };

        // ChildElements returns an IList<Element>
        foreach (Element child in element.ChildElements)
        {
            node.Children.Add(ConvertElement(child));
        }

        return node;
    }
}