using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJsonPath = "tagged_content.json";
        const string outputPdfCopy = "output_copy.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            JsonStructure jsonRoot = BuildJson(root);

            string json = JsonSerializer.Serialize(jsonRoot, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Tagged content exported to {outputJsonPath}");

            // Save a copy of the PDF (no modifications) to satisfy lifecycle rules
            doc.Save(outputPdfCopy);
        }
    }

    static JsonStructure BuildJson(StructureElement element)
    {
        var json = new JsonStructure
        {
            Type = element.GetType().Name,
            Text = element.ActualText,
            AltText = element.AlternativeText,
            Language = element.Language,
            Children = new List<JsonStructure>()
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                json.Children.Add(BuildJson(se));
            }
        }

        return json;
    }

    class JsonStructure
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string AltText { get; set; }
        public string Language { get; set; }
        public List<JsonStructure> Children { get; set; }
    }
}