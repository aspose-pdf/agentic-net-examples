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
    public List<JsonStructureElement> Children { get; set; } = new List<JsonStructureElement>();
}

class Program
{
    static JsonStructureElement ConvertToJsonElement(StructureElement element)
    {
        JsonStructureElement jsonEl = new JsonStructureElement {
            Type = element.GetType().Name,
            Text = element.ActualText,
            AlternativeText = element.AlternativeText
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                jsonEl.Children.Add(ConvertToJsonElement(se));
        }

        return jsonEl;
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "tagged_content.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                ITaggedContent tagged = doc.TaggedContent;

                // Root of the logical structure tree
                StructureElement root = tagged.RootElement;

                // Convert the structure tree to a serializable object
                JsonStructureElement jsonRoot = ConvertToJsonElement(root);

                // Serialize to indented JSON
                string json = JsonSerializer.Serialize(jsonRoot, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write JSON to file
                File.WriteAllText(outputJson, json);
                Console.WriteLine($"Tagged content exported to '{outputJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}