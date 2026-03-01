using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Facades; // Facade namespace included as requested

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
            // Access the tagged‑content interface; it will be null if the PDF is not tagged
            ITaggedContent tagged = doc.TaggedContent;

            if (tagged == null)
            {
                Console.WriteLine("The document is not a tagged PDF.");
                return;
            }

            // Traverse the logical structure tree starting from the root element
            StructureElement root = tagged.RootElement;
            Console.WriteLine("Structure tree:");
            WalkStructure(root, 0);
        }
    }

    // Recursive walk of the structure tree
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}: AltText=\"{element.AlternativeText}\" ActualText=\"{element.ActualText}\"");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}