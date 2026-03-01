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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a structure tree
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The PDF does not contain tagged content.");
                return;
            }

            Console.WriteLine("Traversing Structure Tree Root:");
            TraverseStructure(tagged.RootElement, 0);
        }
    }

    // Recursive traversal of the logical structure tree
    static void TraverseStructure(Element element, int depth)
    {
        // Indentation for visual hierarchy
        string indent = new string(' ', depth * 2);

        // Cast to StructureElement to access accessibility properties
        if (element is StructureElement se)
        {
            string actual = se.ActualText ?? string.Empty;
            string alt    = se.AlternativeText ?? string.Empty;
            string lang   = se.Language ?? string.Empty;
            string title  = se.Title ?? string.Empty;

            Console.WriteLine($"{indent}{se.GetType().Name}: Title=\"{title}\", ActualText=\"{actual}\", AltText=\"{alt}\", Language=\"{lang}\"");
        }
        else
        {
            // For non‑structure elements, just output the type name
            Console.WriteLine($"{indent}{element.GetType().Name}");
        }

        // Recurse into child elements
        foreach (Element child in element.ChildElements)
        {
            TraverseStructure(child, depth + 1);
        }
    }
}