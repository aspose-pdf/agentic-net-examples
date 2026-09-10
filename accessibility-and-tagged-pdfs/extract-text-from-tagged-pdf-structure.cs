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
            // Access tagged content; if null the PDF is not tagged
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.WriteLine("The document does not contain tagged content.");
                return;
            }

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Extracted textual content from the structure tree:");
            TraverseStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and output any text found
    static void TraverseStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string text = element.ActualText ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine($"{indent}{text}");
        }

        // ChildElements returns an ElementList; iterate over it
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                TraverseStructure(childStructure, depth + 1);
            }
        }
    }
}