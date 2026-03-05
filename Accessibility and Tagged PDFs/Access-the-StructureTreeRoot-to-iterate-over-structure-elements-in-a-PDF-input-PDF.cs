using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Use a Facade to bind the PDF, then work with the underlying Document
        using (PdfBookmarkEditor facade = new PdfBookmarkEditor())
        {
            facade.BindPdf(inputPath);
            Document doc = facade.Document; // underlying Document instance

            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Get the StructTreeRootElement
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            Console.WriteLine("StructureTreeRoot hierarchy:");
            Walk(structRoot, 0);
        }
    }

    // Recursive traversal of the logical structure tree
    static void Walk(Element element, int depth)
    {
        string indent = new string(' ', depth * 2);
        // Print element type and optional properties
        if (element is StructureElement se)
        {
            Console.WriteLine($"{indent}{element.GetType().Name}: Title=\"{se.Title}\", AltText=\"{se.AlternativeText}\"");
        }
        else
        {
            Console.WriteLine($"{indent}{element.GetType().Name}");
        }

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            Walk(child, depth + 1);
        }
    }
}