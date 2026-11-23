using System;
using System.Collections;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "childopen.pdf";

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
                // Set Open = true for all direct child bookmarks
                foreach (OutlineItemCollection child in chapterOne)
                {
                    child.Open = true;
                }
            }
            else
            {
                Console.WriteLine("Bookmark \"Chapter One\" not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }

    // Recursively searches an OutlineCollection (or child collection) for a bookmark with the given title
    static OutlineItemCollection FindBookmark(IEnumerable collection, string title)
    {
        foreach (var obj in collection)
        {
            if (obj is OutlineItemCollection item)
            {
                if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
                    return item;

                // Search in child items recursively – an OutlineItemCollection *is* its own child collection
                var foundInChildren = FindBookmark(item, title);
                if (foundInChildren != null)
                    return foundInChildren;
            }
        }
        return null;
    }
}
