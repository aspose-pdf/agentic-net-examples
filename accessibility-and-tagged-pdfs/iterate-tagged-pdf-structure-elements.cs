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
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;
            if (taggedContent == null)
            {
                Console.WriteLine("The document does not contain tagged content.");
                return;
            }

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Recursively walk the structure tree and log information
            WalkStructureElement(root, 0);
        }
    }

    // Recursive helper to process each structure element
    static void WalkStructureElement(StructureElement element, int depth)
    {
        // Indentation for readability
        string indent = new string(' ', depth * 2);

        // Tag name (type name), language, and title (using ActualText as the title)
        string tagName   = element.GetType().Name;
        string language  = element.Language ?? string.Empty;
        string title     = element.ActualText ?? string.Empty;

        Console.WriteLine($"{indent}Tag: {tagName}, Language: {language}, Title: {title}");

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                WalkStructureElement(childStructure, depth + 1);
            }
        }
    }
}