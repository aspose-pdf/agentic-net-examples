using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class JsonStructureElement
{
    public string Type { get; set; }
    public string Text { get; set; }
    public string AlternativeText { get; set; }
    public string Language { get; set; }
    public List<JsonStructureElement> Children { get; set; } = new List<JsonStructureElement>();
}

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_content.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Build a hierarchical representation
            JsonStructureElement jsonRoot = BuildElement(root);

            // Serialize to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonRoot, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Tagged content exported to '{outputPath}'.");
    }

    // Recursively converts a StructureElement into a JSON-friendly DTO
    static JsonStructureElement BuildElement(StructureElement element)
    {
        JsonStructureElement jsonElem = new JsonStructureElement {
            Type            = element.GetType().Name,
            Text            = element.ActualText,
            AlternativeText = element.AlternativeText,
            Language        = element.Language
        };

        // Iterate over child elements using the correct property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                jsonElem.Children.Add(BuildElement(childStruct));
            }
        }

        return jsonElem;
    }
}