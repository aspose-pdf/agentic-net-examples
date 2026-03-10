using System;
using System.IO;
using Aspose.Pdf;                     // Document, Page, etc.
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;     // StructureElement, Element

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

        // Load the PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged‑content interface; it may be null for untagged PDFs
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The document does not contain tagged (accessible) content.");
                return;
            }

            // Root element of the logical structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Example: display language set on the root element (if any)
            Console.WriteLine($"Root Language: {root.Language ?? "(none)"}");

            // Recursively walk the structure tree and output key properties
            WalkStructure(root, 0);
        }
    }

    // Recursive helper to traverse StructureElement hierarchy
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // Show element type and accessibility‑relevant properties
        Console.WriteLine($"{indent}{element.GetType().Name}:");
        Console.WriteLine($"{indent}  ActualText: {element.ActualText}");
        Console.WriteLine($"{indent}  AlternativeText: {element.AlternativeText}");
        Console.WriteLine($"{indent}  Language: {element.Language}");

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}