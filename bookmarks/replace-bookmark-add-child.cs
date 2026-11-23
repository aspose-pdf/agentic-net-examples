using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "modify.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the bookmark titled "Chapter One"
            OutlineItemCollection chapterOne = FindBookmark(doc.Outlines, "Chapter One");
            if (chapterOne != null)
            {
                // Remove the existing bookmark
                doc.Outlines.Remove(chapterOne);
            }

            // Locate the parent bookmark titled "Introduction"
            OutlineItemCollection introduction = FindBookmark(doc.Outlines, "Introduction");
            if (introduction != null)
            {
                // Create a new child bookmark that points to page 4 (1‑based indexing)
                OutlineItemCollection newSection = new OutlineItemCollection(doc.Outlines)
                {
                    Title = "New Section",
                    Action = new GoToAction(doc.Pages[4]) // page 4
                };

                // Attach the new bookmark under "Introduction"
                introduction.Add(newSection);
            }
            else
            {
                Console.Error.WriteLine("Parent bookmark 'Introduction' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    // Recursively searches an outline collection for a bookmark with the given title
    static OutlineItemCollection FindBookmark(IEnumerable<OutlineItemCollection> collection, string title)
    {
        foreach (OutlineItemCollection item in collection)
        {
            if (item.Title == title)
                return item;

            // Search child outlines recursively
            OutlineItemCollection found = FindBookmark(item, title);
            if (found != null)
                return found;
        }
        return null;
    }

    // Overload that accepts a single OutlineItemCollection (its children are enumerable)
    static OutlineItemCollection FindBookmark(OutlineItemCollection parent, string title)
    {
        return FindBookmark((IEnumerable<OutlineItemCollection>)parent, title);
    }
}
