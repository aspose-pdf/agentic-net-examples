using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // DTO with nullable properties to satisfy the compiler warnings
    private class ElementInfo
    {
        public string? ElementType { get; set; }
        public string? ActualText { get; set; }
        public string? AlternativeText { get; set; }
        public string? Language { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "structure_elements.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Check whether the PDF is tagged by verifying TaggedContent is not null
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not a tagged PDF. No structure elements to enumerate.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Retrieve the child elements using the ElementList class
            ElementList children = root.ChildElements;

            // Prepare a list to hold serializable information
            List<ElementInfo> elementInfos = new List<ElementInfo>();

            // Enumerate each child element
            foreach (Element child in children)
            {
                // Process only StructureElement instances
                if (child is StructureElement se)
                {
                    elementInfos.Add(new ElementInfo
                    {
                        ElementType = se.GetType().Name,
                        ActualText = se.ActualText,
                        AlternativeText = se.AlternativeText,
                        // Language property is not available in all versions; keep it null
                        Language = null
                    });
                }
            }

            // Serialize the list to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(elementInfos, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Structure elements written to '{outputJsonPath}'.");
        }
    }
}
