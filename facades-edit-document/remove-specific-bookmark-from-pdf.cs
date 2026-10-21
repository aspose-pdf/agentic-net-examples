using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Draft Outline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the outline (bookmark) with the specified title
            OutlineItemCollection item = FindOutlineItem(doc.Outlines, bookmarkTitle);
            if (item != null)
            {
                // Remove the found bookmark from the document's outline collection
                // OutlineCollection.Delete(string) removes by title; we already know the title exists.
                doc.Outlines.Delete(bookmarkTitle);
                Console.WriteLine($"Bookmark '{bookmarkTitle}' removed.");
            }
            else
            {
                Console.WriteLine($"Bookmark '{bookmarkTitle}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved to '{outputPath}'.");
    }

    /// <summary>
    /// Recursively searches an outline hierarchy for an item with the given title.
    /// Returns the first matching OutlineItemCollection or null if not found.
    /// </summary>
    private static OutlineItemCollection FindOutlineItem(IEnumerable<OutlineItemCollection> outlines, string title)
    {
        foreach (OutlineItemCollection item in outlines)
        {
            if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
                return item;

            // Search child outlines recursively – an OutlineItemCollection itself is enumerable over its children.
            if (item != null)
            {
                var found = FindOutlineItem(item, title);
                if (found != null)
                    return found;
            }
        }
        return null;
    }
}
