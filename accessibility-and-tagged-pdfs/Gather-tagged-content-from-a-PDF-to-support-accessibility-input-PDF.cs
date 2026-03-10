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
            // Check if the document contains tagged content
            // In recent Aspose.PDF versions the Document class does not expose an IsTagged property.
            // Instead we verify that the TaggedContent interface is available and that it has a root element.
            if (doc.TaggedContent == null || doc.TaggedContent.RootElement == null)
            {
                Console.WriteLine("The PDF is not tagged. No structure to extract.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Tagged structure tree:");
            Walk(root, 0);
        }
    }

    // Recursively walk the structure tree and output element information
    static void Walk(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;

        Console.WriteLine($"{indent}{typeName}: Text=\"{actualText}\" Alt=\"{altText}\"");

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                Walk(childStruct, depth + 1);
            }
        }
    }
}
