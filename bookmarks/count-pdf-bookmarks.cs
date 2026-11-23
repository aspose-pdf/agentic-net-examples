using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "total_bookmarks.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Count all bookmarks (outline items) recursively.
            int totalBookmarks = 0;
            foreach (OutlineItemCollection topItem in doc.Outlines)
            {
                totalBookmarks += CountBookmarks(topItem);
            }

            // Write the count to a text file.
            File.WriteAllText(outputPath, totalBookmarks.ToString());

            Console.WriteLine($"Total bookmarks: {totalBookmarks}");
            Console.WriteLine($"Result written to '{outputPath}'.");
        }
    }

    // Recursively counts a bookmark (OutlineItemCollection) and all of its children.
    private static int CountBookmarks(OutlineItemCollection item)
    {
        int count = 1; // Count the current bookmark.
        foreach (OutlineItemCollection child in item)
        {
            count += CountBookmarks(child);
        }
        return count;
    }
}
