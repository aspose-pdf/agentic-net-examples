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
            // Check if the document contains a tagged structure.
            // In older Aspose.PDF versions the Document class does not expose an IsTagged property.
            // Instead we verify that TaggedContent is available and has a root element.
            if (doc.TaggedContent == null || doc.TaggedContent.RootElement == null)
            {
                Console.WriteLine("The PDF is not tagged.");
                return;
            }

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Tagged structure tree:");
            WalkStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and output element information
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;

        Console.WriteLine($"{indent}{typeName}: text=\"{actualText}\" alt=\"{altText}\"");

        // ChildElements returns an ElementList; iterate and process nested StructureElements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                WalkStructure(se, depth + 1);
            }
        }
    }
}
