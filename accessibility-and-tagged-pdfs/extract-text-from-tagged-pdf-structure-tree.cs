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
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Verify that the document contains a structure tree
            if (tagged?.RootElement == null)
            {
                Console.WriteLine("The PDF is not tagged or has no structure tree.");
                return;
            }

            // Start traversal from the root element
            StructureElement root = tagged.RootElement;
            Console.WriteLine("Extracted text from the structure tree:");
            TraverseStructure(root, 0);
        }
    }

    // Recursively walk the structure tree and output text from each element
    static void TraverseStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // Prefer ActualText; fall back to AlternativeText if ActualText is empty
        string text = element.ActualText ?? element.AlternativeText ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine($"{indent}{element.GetType().Name}: {text}");
        }

        // Process child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                TraverseStructure(childStruct, depth + 1);
            }
        }
    }
}