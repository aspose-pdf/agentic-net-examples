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
        const string jsonPath  = "structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking TaggedContent (IsTagged does not exist in older versions)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure tree to export.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement; // No cast needed

            // Build a serializable model of the structure tree
            StructureNode model = BuildElementModel(root);

            // Serialize to indented JSON
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(model, options);

            // Write JSON to file
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Structure tree exported to '{jsonPath}'.");
        }
    }

    // Model class used for JSON serialization
    class StructureNode
    {
        public string ElementType      { get; set; }
        public string Text             { get; set; }
        public string AlternativeText  { get; set; }
        // Language property removed because it is not available in all Aspose.Pdf versions
        public List<StructureNode> Children { get; set; } = new List<StructureNode>();
    }

    // Recursively convert Aspose.Pdf.LogicalStructure elements to the model
    static StructureNode BuildElementModel(StructureElement element)
    {
        StructureNode node = new StructureNode {
            ElementType      = element.GetType().Name,
            Text             = element.ActualText,
            AlternativeText = element.AlternativeText
            // Language omitted for compatibility
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                node.Children.Add(BuildElementModel(se));
            }
        }

        return node;
    }
}
