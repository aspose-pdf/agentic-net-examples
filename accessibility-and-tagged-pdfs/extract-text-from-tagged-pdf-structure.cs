using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine if the PDF is tagged by checking TaggedContent (IsTagged does not exist in this version)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged; no structure tree to traverse.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            Console.WriteLine("Extracted textual content from the structure tree:");
            TraverseStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and output text/alt text
    static void TraverseStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // ActualText holds the visible text of the element
        string actualText = element.ActualText ?? string.Empty;

        // AlternativeText holds alt text for images or other non‑text elements
        string altText = element.AlternativeText ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(actualText) || !string.IsNullOrWhiteSpace(altText))
        {
            Console.WriteLine($"{indent}[{element.GetType().Name}] Text: '{actualText}' Alt: '{altText}'");
        }

        // Recurse into child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                TraverseStructure(childStruct, depth + 1);
            }
        }
    }
}