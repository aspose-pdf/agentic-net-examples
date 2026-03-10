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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the StructTreeRootElement (the entry point for the logical structure)
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            Console.WriteLine("Traversing StructureTreeRoot:");
            Traverse(structRoot, 0);
        }
    }

    // Recursive traversal of structure elements
    static void Traverse(Element element, int depth)
    {
        // Indentation for readability
        string indent = new string(' ', depth * 2);

        // "ActualText" is defined on StructureElement, not on the base Element class.
        // Safely cast to StructureElement to read that property; otherwise leave it empty.
        string actual = string.Empty;
        if (element is StructureElement structElem)
        {
            actual = structElem.ActualText ?? string.Empty;
        }

        Console.WriteLine($"{indent}{element.GetType().Name}: ActualText=\"{actual}\"");

        // Iterate over child elements using the ChildElements collection
        foreach (Element child in element.ChildElements)
        {
            Traverse(child, depth + 1);
        }
    }
}
