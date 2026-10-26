using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const string bookmarkId  = "bookmark123"; // Title (or Name) of the bookmark to modify
        const int    targetPage  = 10;            // Aspose.Pdf uses 1‑based page indexing

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Locate the bookmark (outline item) by its title/ID
        OutlineItemCollection bookmark = FindBookmarkByTitle(doc.Outlines, bookmarkId);
        if (bookmark == null)
        {
            Console.Error.WriteLine($"Bookmark with ID '{bookmarkId}' not found.");
            return;
        }

        // Update the bookmark's destination to point to the desired page
        // GoToAction(Page) creates a destination that points to the given page
        bookmark.Action = new GoToAction(doc.Pages[targetPage]);

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Bookmark '{bookmarkId}' now points to page {targetPage} and was saved to '{outputPath}'.");
    }

    // Recursively searches the outline tree for a bookmark with the specified title
    private static OutlineItemCollection FindBookmarkByTitle(IEnumerable<OutlineItemCollection> collection, string title)
    {
        foreach (OutlineItemCollection item in collection)
        {
            if (item.Title == title)
                return item;

            // Search child outlines recursively (each OutlineItemCollection is also IEnumerable<OutlineItemCollection>)
            OutlineItemCollection found = FindBookmarkByTitle(item, title);
            if (found != null)
                return found;
        }
        return null;
    }
}
