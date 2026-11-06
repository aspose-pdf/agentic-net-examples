using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";          // PDF to which bookmarks will be added
        const string sourcePath = "source.pdf";          // PDF containing the bookmarks to import
        const string outputPath = "merged_bookmarks.pdf"; // Resulting PDF

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both PDFs
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Merge bookmarks preserving the order found in the source PDF
            CopyOutlines(sourceDoc.Outlines, targetDoc.Outlines, targetDoc);

            // Save the merged PDF
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks merged and saved to '{outputPath}'.");
    }

    // Recursively copies outline items from a source collection to a destination collection.
    private static void CopyOutlines(
        OutlineCollection sourceCollection,
        OutlineCollection destinationCollection,
        Document targetDoc)
    {
        foreach (OutlineItemCollection srcItem in sourceCollection)
        {
            // Create a new outline item for the target document and copy basic properties.
            OutlineItemCollection destItem = new OutlineItemCollection(targetDoc.Outlines)
            {
                Title = srcItem.Title,
                Destination = srcItem.Destination,
                Italic = srcItem.Italic,
                Bold = srcItem.Bold,
                Color = srcItem.Color,
                Open = srcItem.Open
            };

            // Add the new item to the destination collection.
            destinationCollection.Add(destItem);

            // If the source item has children, copy them recursively.
            if (srcItem.Count > 0)
            {
                CopyOutlines(srcItem, destItem, targetDoc);
            }
        }
    }

    // Overload that works when the parent is an OutlineItemCollection (i.e., a child node).
    private static void CopyOutlines(
        OutlineItemCollection sourceParent,
        OutlineItemCollection destParent,
        Document targetDoc)
    {
        foreach (OutlineItemCollection srcItem in sourceParent)
        {
            OutlineItemCollection destItem = new OutlineItemCollection(targetDoc.Outlines)
            {
                Title = srcItem.Title,
                Destination = srcItem.Destination,
                Italic = srcItem.Italic,
                Bold = srcItem.Bold,
                Color = srcItem.Color,
                Open = srcItem.Open
            };

            destParent.Add(destItem);

            if (srcItem.Count > 0)
            {
                CopyOutlines(srcItem, destItem, targetDoc);
            }
        }
    }
}
