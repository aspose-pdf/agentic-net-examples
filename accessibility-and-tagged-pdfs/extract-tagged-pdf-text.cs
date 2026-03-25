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

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            Console.WriteLine("Tagged PDF text content:");
            Traverse(root, 0);
        }
    }

    static void Traverse(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string text = element.ActualText ?? string.Empty;
        Console.WriteLine($"{indent}{element.GetType().Name}: \"{text}\"");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                Traverse(se, depth + 1);
        }
    }
}