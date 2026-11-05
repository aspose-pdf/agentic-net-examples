using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for possible actions, not required for printing titles

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The document outline (bookmarks) is accessed via the Outlines collection
            if (doc.Outlines == null || doc.Outlines.Count == 0)
            {
                Console.WriteLine("No outline (bookmark) hierarchy found in the PDF.");
                return;
            }

            Console.WriteLine("PDF Outline Hierarchy:");
            foreach (OutlineItemCollection rootItem in doc.Outlines)
            {
                PrintOutlineItem(rootItem, 0);
            }
        }
    }

    // Recursively prints an outline item and its children with indentation
    static void PrintOutlineItem(OutlineItemCollection item, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}- {item.Title}");

        // Child items are enumerated directly because OutlineItemCollection *is* its own collection
        foreach (OutlineItemCollection child in item)
        {
            PrintOutlineItem(child, depth + 1);
        }
    }
}
