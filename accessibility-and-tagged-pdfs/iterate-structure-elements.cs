using System;
using System.IO;
using System.Reflection;
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

            Console.WriteLine("Structure elements:");
            Walk(root);
        }
    }

    static void Walk(StructureElement element)
    {
        string title = element.Title ?? string.Empty;
        string language = element.Language ?? string.Empty;
        string tag = string.Empty;

        // Attempt to read custom tag if the property exists
        PropertyInfo tagProp = element.GetType().GetProperty("Tag");
        if (tagProp != null)
        {
            object value = tagProp.GetValue(element);
            tag = value as string ?? string.Empty;
        }

        Console.WriteLine($"Title: '{title}', Language: '{language}', Tag: '{tag}'");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                Walk(se);
        }
    }
}