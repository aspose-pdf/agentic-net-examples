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

        // Load the PDF document (using the lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Structure Elements:");
            WalkStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and log Title, Language, and StructureType (tag name)
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string title = element.Title ?? "(no title)";
        string language = element.Language ?? "(no language)";
        string tagName = element.StructureType?.ToString() ?? "(no tag)";

        Console.WriteLine($"{indent}Tag: {tagName}, Title: {title}, Language: {language}");

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