using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "intro_children.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Locate the bookmark titled "Introduction"
            OutlineItemCollection introBookmark = FindBookmarkByTitle(doc.Outlines, "Introduction");
            if (introBookmark == null)
            {
                Console.WriteLine("Bookmark 'Introduction' not found.");
                return;
            }

            // Write titles of all direct child bookmarks to the output file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                foreach (OutlineItemCollection child in introBookmark)
                {
                    writer.WriteLine(child.Title);
                }
            }

            Console.WriteLine($"Child bookmark titles written to {outputPath}");
        }
    }

    // Search recursively in an OutlineCollection for a bookmark with the specified title
    static OutlineItemCollection FindBookmarkByTitle(OutlineCollection outlines, string title)
    {
        foreach (OutlineItemCollection item in outlines)
        {
            if (item.Title == title)
                return item;

            OutlineItemCollection found = FindBookmarkByTitle(item, title);
            if (found != null)
                return found;
        }
        return null;
    }

    // Search recursively in an OutlineItemCollection's children for a bookmark with the specified title
    static OutlineItemCollection FindBookmarkByTitle(OutlineItemCollection parent, string title)
    {
        foreach (OutlineItemCollection child in parent)
        {
            if (child.Title == title)
                return child;

            OutlineItemCollection deeper = FindBookmarkByTitle(child, title);
            if (deeper != null)
                return deeper;
        }
        return null;
    }
}