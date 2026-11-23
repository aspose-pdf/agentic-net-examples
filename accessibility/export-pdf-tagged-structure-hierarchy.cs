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
        const string outputPath = "structure.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains a tagged structure (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure tree to export.");
                return;
            }

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Write the hierarchy to a plain‑text file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                WriteElement(root, writer, 0);
            }

            Console.WriteLine($"Structure hierarchy exported to '{outputPath}'.");
        }
    }

    // Recursively writes an element and its children with indentation
    static void WriteElement(StructureElement element, StreamWriter writer, int depth)
    {
        string indent = new string(' ', depth * 2);
        string actual = element.ActualText ?? string.Empty;
        string alt    = element.AlternativeText ?? string.Empty;

        // Output element type and any associated text
        writer.WriteLine($"{indent}{element.GetType().Name}: {actual}{(string.IsNullOrEmpty(alt) ? "" : $" (Alt: {alt})")}");

        // Iterate over child elements using ChildElements (not Elements)
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                WriteElement(se, writer, depth + 1);
            }
        }
    }
}
