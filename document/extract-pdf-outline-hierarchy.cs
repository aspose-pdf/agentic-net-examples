using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            OutlineCollection outlines = doc.Outlines;

            if (outlines.VisibleCount == 0)
            {
                Console.WriteLine("The document has no outline entries.");
                return;
            }

            Console.WriteLine("Document Outline:");
            // Iterate over top‑level outline items
            foreach (OutlineItemCollection item in outlines)
            {
                PrintOutline(item, string.Empty);
            }
        }
    }

    // Recursively prints an outline item and its children with indentation
    static void PrintOutline(OutlineItemCollection item, string indent)
    {
        // Title may be null; fallback to empty string
        string title = item.Title ?? "(no title)";
        Console.WriteLine($"{indent}- {title}");

        // Each OutlineItemCollection can contain child outline items.
        // Iterate over them recursively.
        foreach (OutlineItemCollection child in item)
        {
            PrintOutline(child, indent + "  ");
        }
    }
}