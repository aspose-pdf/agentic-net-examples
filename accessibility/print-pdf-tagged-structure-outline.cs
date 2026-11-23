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
            // Check if the document contains a tagged structure tree
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged; no structure hierarchy to display.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Structure Outline:");
            PrintElement(root, 0);
        }
    }

    // Recursively prints each structure element with indentation
    static void PrintElement(StructureElement element, int depth)
    {
        // Two spaces per hierarchy level
        string indent = new string(' ', depth * 2);

        // Prefer ActualText; fall back to AlternativeText if ActualText is null/empty
        string text = element.ActualText ?? element.AlternativeText ?? string.Empty;

        // Output element type and its associated text
        Console.WriteLine($"{indent}{element.GetType().Name}: {text}");

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                PrintElement(se, depth + 1);
            }
        }
    }
}
