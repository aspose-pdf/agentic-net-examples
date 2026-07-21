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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content interface
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.WriteLine("The document does not contain tagged content.");
                return;
            }

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            StringBuilder sb = new StringBuilder();
            TraverseStructure(root, sb, 0);

            Console.WriteLine("Extracted textual content from the tagged PDF:");
            Console.WriteLine(sb.ToString());
        }
    }

    // Recursively walk the structure tree and collect text
    static void TraverseStructure(StructureElement element, StringBuilder sb, int depth)
    {
        // Indentation for readability (optional)
        string indent = new string(' ', depth * 2);

        // ActualText holds the visible text of the element
        string text = element.ActualText ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(text))
        {
            sb.AppendLine($"{indent}{text}");
        }

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                TraverseStructure(childStruct, sb, depth + 1);
            }
        }
    }
}