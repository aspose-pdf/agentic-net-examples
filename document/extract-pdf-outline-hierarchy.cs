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

            if (outlines.Count == 0)
            {
                Console.WriteLine("The document has no outline entries.");
                return;
            }

            // Start traversal from the first top‑level outline item
            PrintOutline(outlines.First, 0);
        }
    }

    // Recursively prints an outline item and its children as a tree
    static void PrintOutline(OutlineItemCollection item, int depth)
    {
        if (item == null) return;

        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{item.Title}");

        // Process child items (first child of the current item)
        if (item.First != null)
            PrintOutline(item.First, depth + 1);

        // Process sibling items (next item at the same level)
        if (item.Next != null)
            PrintOutline(item.Next, depth);
    }
}