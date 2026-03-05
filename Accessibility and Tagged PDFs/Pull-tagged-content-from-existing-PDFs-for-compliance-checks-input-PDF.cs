using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use provided load)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (no explicit cast, correct namespace)
            ITaggedContent tagged = doc.TaggedContent;

            // Basic compliance information
            Console.WriteLine($"PDF/UA compliant : {doc.IsPdfUaCompliant}");
            Console.WriteLine($"PDF/A compliant  : {doc.IsPdfaCompliant}");
            Console.WriteLine($"Title            : {doc.Info.Title}");
            Console.WriteLine($"Author           : {doc.Info.Author}");

            // Traverse the logical structure tree
            StructureElement root = tagged.RootElement;
            Console.WriteLine("Tagged structure tree:");
            WalkStructure(root, 0);

            // Save the document (lifecycle rule: use provided save)
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }

    // Recursive walk of structure elements using ChildElements (correct property)
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string actual = element.ActualText ?? string.Empty;
        string alt    = element.AlternativeText ?? string.Empty;

        Console.WriteLine($"{indent}{element.GetType().Name}: text='{actual}' alt='{alt}'");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}