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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Display basic document metadata (available via Document.Info)
            Console.WriteLine($"Title (Info): {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");
            Console.WriteLine($"Subject: {doc.Info.Subject}");

            // Indicate that tagged content is present (ITaggedContent always exists)
            Console.WriteLine("Tagged content present.");

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Recursively walk the structure tree and output element details
            Console.WriteLine("Structure tree:");
            WalkStructure(root, 0);
        }
    }

    // Recursive traversal of StructureElement hierarchy
    static void WalkStructure(StructureElement element, int depth)
    {
        // Indentation for readability
        string indent = new string(' ', depth * 2);

        // Retrieve element type and accessible text properties
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;

        // Output element information
        Console.WriteLine($"{indent}{typeName}: text=\"{actualText}\" alt=\"{altText}\"");

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                WalkStructure(se, depth + 1);
            }
        }
    }
}