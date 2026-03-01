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

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Access tagged content (if any)
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged == null || tagged.RootElement == null)
                {
                    Console.WriteLine("The document does not contain tagged (accessible) content.");
                    return;
                }

                // Root of the structure tree
                StructureElement root = tagged.RootElement;
                Console.WriteLine("Structure tree:");
                WalkStructure(root, 0);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively walk the logical structure tree and output element details
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string typeName = element.GetType().Name;
        string actualText = element.ActualText ?? string.Empty;
        string altText = element.AlternativeText ?? string.Empty;
        string language = element.Language ?? string.Empty;

        Console.WriteLine($"{indent}{typeName}: Text=\"{actualText}\" Alt=\"{altText}\" Lang=\"{language}\"");

        // Iterate child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                WalkStructure(se, depth + 1);
            }
        }
    }
}