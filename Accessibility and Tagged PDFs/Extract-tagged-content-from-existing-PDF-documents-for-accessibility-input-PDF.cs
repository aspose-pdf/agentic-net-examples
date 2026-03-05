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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // If the document has no tagged structure, inform the user
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The PDF does not contain tagged content.");
                return;
            }

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Tagged structure tree:");
            WalkStructure(root, 0);
        }
    }

    // Recursive traversal of the structure tree
    static void WalkStructure(StructureElement element, int depth)
    {
        // Indentation for visual hierarchy
        string indent = new string(' ', depth * 2);

        // Retrieve useful properties
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;

        // Print element information
        Console.WriteLine($"{indent}{typeName}: Text=\"{actualText}\" Alt=\"{altText}\"");

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                WalkStructure(childStruct, depth + 1);
            }
        }
    }
}