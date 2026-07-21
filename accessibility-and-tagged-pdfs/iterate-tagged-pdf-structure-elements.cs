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

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Structure elements:");
            WalkStructure(root, 0);
        }
    }

    // Recursive traversal of the structure tree
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // Retrieve properties; Title, Language, and custom tag (if available)
        string title = element.Title ?? string.Empty;
        string language = element.Language ?? string.Empty;
        string customTag = string.Empty;

        // Attempt to get the custom tag name; SetTag exists, GetTag is not documented
        // but many versions expose a GetTag() method. Use it if available.
        try
        {
            // This call will compile only if GetTag() exists; otherwise it will be ignored.
            customTag = (element as dynamic).GetTag() ?? string.Empty;
        }
        catch { /* ignore if method not present */ }

        Console.WriteLine($"{indent}Element: {element.GetType().Name}");
        Console.WriteLine($"{indent}  Title   : {title}");
        Console.WriteLine($"{indent}  Language: {language}");
        Console.WriteLine($"{indent}  Tag     : {customTag}");

        // Recurse into child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}