using System;
using System.IO;
using Aspose.Pdf;

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
            // Get the outline hierarchy
            Aspose.Pdf.OutlineCollection outlines = doc.Outlines;

            if (outlines.Count == 0)
            {
                Console.WriteLine("No outline entries found.");
                return;
            }

            // Start with the first top‑level outline item
            Aspose.Pdf.OutlineItemCollection first = outlines.First;
            PrintOutline(first, 0);
        }
    }

    // Recursively prints an outline item and its descendants as a tree
    static void PrintOutline(Aspose.Pdf.OutlineItemCollection item, int depth)
    {
        Aspose.Pdf.OutlineItemCollection current = item;
        while (current != null)
        {
            // Indent according to hierarchy depth
            string indent = new string(' ', depth * 2);
            Console.WriteLine($"{indent}- {current.Title}");

            // If the item has child entries, print them with increased depth
            if (current.First != null)
            {
                PrintOutline(current.First, depth + 1);
            }

            // Move to the next sibling at the same level
            current = current.Next;
        }
    }
}