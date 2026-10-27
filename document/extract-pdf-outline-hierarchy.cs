using System;
using System.Collections.Generic;
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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // The outline (bookmark) hierarchy is stored in doc.Outlines.
            // If there are no outlines, inform the user.
            if (doc.Outlines == null || doc.Outlines.Count == 0)
            {
                Console.WriteLine("The document does not contain any outlines.");
                return;
            }

            Console.WriteLine("Outline hierarchy:");
            PrintOutlineItems(doc.Outlines, string.Empty);
        }
    }

    // Recursively prints outline items with indentation to visualize the tree.
    static void PrintOutlineItems(IEnumerable<OutlineItemCollection> items, string indent)
    {
        foreach (OutlineItemCollection item in items)
        {
            // Display the title of the outline entry.
            Console.WriteLine($"{indent}- {item.Title}");

            // If the item has child outlines, recurse with increased indentation.
            if (item.Count > 0)
            {
                PrintOutlineItems(item, indent + "  ");
            }
        }
    }
}
