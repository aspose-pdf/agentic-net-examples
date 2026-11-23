using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction (used by outline actions)

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "openthendelete.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Expand all bookmarks (outline items)
            ExpandAllOutlines(doc.Outlines);

            // Delete the bookmark titled "Appendix A"
            DeleteOutlineByTitle(doc.Outlines, "Appendix A");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }

    // Recursively set Open = true for every outline item (root collection)
    static void ExpandAllOutlines(OutlineCollection outlines)
    {
        foreach (OutlineItemCollection item in outlines)
        {
            item.Open = true;
            if (item.Count > 0)
                ExpandAllOutlines(item);
        }
    }

    // Recursively set Open = true for every outline item (child collection)
    static void ExpandAllOutlines(OutlineItemCollection item)
    {
        foreach (OutlineItemCollection child in item)
        {
            child.Open = true;
            if (child.Count > 0)
                ExpandAllOutlines(child);
        }
    }

    // Recursively find and remove outline items with the specified title (root collection)
    static void DeleteOutlineByTitle(OutlineCollection outlines, string title)
    {
        for (int i = outlines.Count - 1; i >= 0; i--)
        {
            var item = outlines[i];
            if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                // Remove the item from its parent collection
                item.Delete();
            }
            else if (item.Count > 0)
            {
                DeleteOutlineByTitle(item, title);
            }
        }
    }

    // Recursively find and remove outline items with the specified title (child collection)
    static void DeleteOutlineByTitle(OutlineItemCollection parent, string title)
    {
        for (int i = parent.Count - 1; i >= 0; i--)
        {
            var item = parent[i];
            if (string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                item.Delete();
            }
            else if (item.Count > 0)
            {
                DeleteOutlineByTitle(item, title);
            }
        }
    }
}
