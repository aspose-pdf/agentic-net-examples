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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: display standard document metadata
            Console.WriteLine($"Document Title: {doc.Info.Title}");
            Console.WriteLine($"Document Author: {doc.Info.Author}");

            // Get the root of the logical structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Recursively walk the structure and output text/alternative text
            WalkStructure(root, 0);
        }
    }

    // Recursive traversal of structure elements
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string actual = element.ActualText ?? string.Empty;
        string alt    = element.AlternativeText ?? string.Empty;

        Console.WriteLine($"{indent}{element.GetType().Name}: text=\"{actual}\" alt=\"{alt}\"");

        // ChildElements is the correct collection to iterate
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}