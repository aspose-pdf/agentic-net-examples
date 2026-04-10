// ------------------------------------------------------------
// File: AsposePdfApi.GlobalUsings.g.cs (auto‑generated placeholder)
// ------------------------------------------------------------
// This file satisfies the compiler expectation for the generated
// global‑usings source file. It declares the same namespaces that are
// used in the main program so that the project can compile without
// relying on the SDK‑generated file.

// <auto‑generated/>
global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Text.Json;
global using Aspose.Pdf;
global using Aspose.Pdf.Tagged;
global using Aspose.Pdf.LogicalStructure;

// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class TaggedNode
{
    // Initialise non‑nullable strings to avoid CS8618 warnings.
    public string ElementType { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string? AlternativeText { get; set; }
    public List<TaggedNode> Children { get; set; } = new();
}

class Program
{
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
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Root of the logical structure tree
                StructureElement root = tagged.RootElement;

                // Build a hierarchical model
                TaggedNode rootNode = BuildNode(root);

                // Serialize hierarchy to JSON (indented for readability)
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(rootNode, jsonOptions);

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

    // Recursively convert StructureElement hierarchy to TaggedNode hierarchy
    static TaggedNode BuildNode(StructureElement element)
    {
        TaggedNode node = new TaggedNode
        {
            ElementType = element.GetType().Name,
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
