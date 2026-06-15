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

            // Start printing from the first top‑level outline item
            PrintOutline(outlines.First, 0);
        }
    }

    // Recursively prints an outline item and its children with indentation
    static void PrintOutline(OutlineItemCollection item, int indentLevel)
    {
        while (item != null)
        {
            // Indentation based on hierarchy depth
            string indent = new string(' ', indentLevel * 2);
            Console.WriteLine($"{indent}- {item.Title}");

            // If the item has child entries, recurse into them
            if (item.First != null)
                PrintOutline(item.First, indentLevel + 1);

            // Move to the next sibling at the same level
            item = item.Next;
        }
    }
}