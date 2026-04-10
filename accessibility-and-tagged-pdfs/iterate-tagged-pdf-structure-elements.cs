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
            ITaggedContent tagged = doc.TaggedContent;

            // Verify that the document contains a tagged structure
            if (tagged?.RootElement == null)
            {
                Console.WriteLine("The PDF does not contain tagged content.");
                return;
            }

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Recursively walk the structure tree and log details
            WalkStructure(root, 0);
        }
    }

    // Recursive traversal of structure elements
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // Title, Language and custom Tag (if present)
        string title    = element.Title    ?? "(none)";
        string language = element.Language ?? "(none)";

        // Custom tag is set via SetTag(string). Retrieve via reflection to avoid compile‑time dependency.
        string tag = element.GetType()
                            .GetProperty("Tag")
                            ?.GetValue(element) as string ?? "(none)";

        Console.WriteLine($"{indent}Element: {element.GetType().Name}");
        Console.WriteLine($"{indent}  Title   : {title}");
        Console.WriteLine($"{indent}  Language: {language}");
        Console.WriteLine($"{indent}  Tag     : {tag}");

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}