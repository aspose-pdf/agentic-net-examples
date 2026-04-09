using System;
using System.IO;
using System.Text;
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
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the logical structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // StringBuilder to accumulate extracted text
            StringBuilder sb = new StringBuilder();

            // Recursively walk the structure tree and collect text
            WalkStructure(root, sb, 0);

            // Output the extracted text
            Console.WriteLine("Extracted text from tagged PDF:");
            Console.WriteLine(sb.ToString());
        }
    }

    // Recursive traversal of structure elements
    static void WalkStructure(StructureElement element, StringBuilder sb, int depth)
    {
        // Optional indentation for readability
        string indent = new string(' ', depth * 2);

        // Append element type and its actual text if available
        if (!string.IsNullOrEmpty(element.ActualText))
        {
            sb.AppendLine($"{indent}[{element.GetType().Name}] {element.ActualText}");
        }

        // Iterate over child elements using ChildElements (not Elements)
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                WalkStructure(se, sb, depth + 1);
            }
        }
    }
}