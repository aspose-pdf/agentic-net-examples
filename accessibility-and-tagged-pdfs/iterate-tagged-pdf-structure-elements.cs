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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content via the ITaggedContent interface
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure (no cast needed)
            StructureElement root = tagged.RootElement;

            // Recursively walk the structure tree and log details
            WalkStructure(root);
        }
    }

    // Recursive traversal of structure elements
    static void WalkStructure(StructureElement element, int depth = 0)
    {
        string indent = new string(' ', depth * 2);

        // Retrieve Title and Language (both may be null)
        string title    = element.Title    ?? "(none)";
        string language = element.Language ?? "(none)";

        // Custom tag retrieval – not exposed directly, so use reflection if a 'Tag' property exists
        string customTag = "(none)";
        PropertyInfo tagProp = element.GetType().GetProperty("Tag", BindingFlags.Public | BindingFlags.Instance);
        if (tagProp != null)
        {
            object value = tagProp.GetValue(element);
            if (value is string s && !string.IsNullOrEmpty(s))
                customTag = s;
        }

        // Log the information
        Console.WriteLine($"{indent}Element: {element.GetType().Name}");
        Console.WriteLine($"{indent}  Title   : {title}");
        Console.WriteLine($"{indent}  Language: {language}");
        Console.WriteLine($"{indent}  Tag     : {customTag}");

        // Iterate over child elements using the ChildElements collection
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}