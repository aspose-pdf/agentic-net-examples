using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "refupdated.pdf";
        const string bookmarkTitle = "References";
        const int targetPageNumber = 48; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the bookmark (outline item) with the specified title
            OutlineItemCollection targetBookmark = FindBookmarkByTitle(doc.Outlines, bookmarkTitle);
            if (targetBookmark == null)
            {
                Console.Error.WriteLine($"Bookmark \"{bookmarkTitle}\" not found.");
                return;
            }

            // Update its destination to the desired page number (page 48)
            // XYZExplicitDestination(page, left, top, zoom). Left/Top set to 0, zoom to 1 (default view).
            targetBookmark.Destination = new XYZExplicitDestination(doc.Pages[targetPageNumber], 0, 0, 1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" updated to page {targetPageNumber} and saved as \"{outputPath}\".");
    }

    // Recursive search through outline items to locate a bookmark by title (root collection)
    static OutlineItemCollection FindBookmarkByTitle(OutlineCollection outlines, string title)
    {
        foreach (OutlineItemCollection item in outlines)
        {
            if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
                return item;

            // Search children recursively
            var found = FindBookmarkByTitle(item, title);
            if (found != null)
                return found;
        }
        return null;
    }

    // Recursive search through children of a given outline item
    static OutlineItemCollection FindBookmarkByTitle(OutlineItemCollection parent, string title)
    {
        foreach (OutlineItemCollection child in parent)
        {
            if (string.Equals(child.Title, title, StringComparison.OrdinalIgnoreCase))
                return child;

            var found = FindBookmarkByTitle(child, title);
            if (found != null)
                return found;
        }
        return null;
    }
}
