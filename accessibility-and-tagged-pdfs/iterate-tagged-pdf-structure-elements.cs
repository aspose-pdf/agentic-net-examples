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

            // If the document is not tagged, there will be no structure tree to traverse
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("The PDF does not contain tagged content.");
                return;
            }

            // Start recursive traversal from the root element
            StructureElement root = tagged.RootElement;
            Console.WriteLine("Structure Elements:");
            WalkStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and log Title, Language, and (if needed) custom tag info
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string title = element.Title ?? "(no title)";
        string language = element.Language ?? "(no language)";

        // Custom tags can be set via SetTag(string) but there is no getter.
        // Therefore we log that a custom tag may exist but cannot be retrieved directly.
        Console.WriteLine($"{indent}Element: {element.GetType().Name}");
        Console.WriteLine($"{indent}  Title   : {title}");
        Console.WriteLine($"{indent}  Language: {language}");
        Console.WriteLine($"{indent}  (Custom tag not directly readable via API)");

        // Iterate over child elements using the ChildElements collection
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                WalkStructure(childStruct, depth + 1);
            }
        }
    }
}