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
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve the StructTreeRootElement (the root of the logical structure tree)
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            if (structRoot == null)
            {
                Console.WriteLine("The document does not contain a structure tree.");
                return;
            }

            Console.WriteLine("Structure tree elements:");
            Walk(structRoot, 0);
        }
    }

    // Recursively walk the structure tree and display element information
    static void Walk(Element element, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}");

        // If the element is a StructureElement we can read its text properties
        if (element is StructureElement se)
        {
            if (!string.IsNullOrEmpty(se.ActualText))
                Console.WriteLine($"{indent}  ActualText: {se.ActualText}");
            if (!string.IsNullOrEmpty(se.AlternativeText))
                Console.WriteLine($"{indent}  AlternativeText: {se.AlternativeText}");
        }

        // Iterate over child elements using ChildElements (not Elements)
        foreach (Element child in element.ChildElements)
        {
            Walk(child, depth + 1);
        }
    }
}