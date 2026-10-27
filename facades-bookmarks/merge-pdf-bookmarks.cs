using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPdfPath = "target.pdf";   // PDF that already has bookmarks
        const string sourcePdfPath = "source.pdf";   // PDF whose bookmarks will be imported
        const string outputPdfPath = "merged_bookmarks.pdf";

        // Verify files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Load the PDFs using the Document class (the recommended way for bookmark manipulation)
        Document targetDoc = new Document(targetPdfPath);
        Document sourceDoc = new Document(sourcePdfPath);

        // Merge bookmarks from source into target while preserving order
        foreach (OutlineItemCollection srcBookmark in sourceDoc.Outlines)
        {
            // Clone each bookmark (and its children) into the target document's outline collection
            OutlineItemCollection cloned = CloneOutlineItem(srcBookmark, targetDoc);
            targetDoc.Outlines.Add(cloned);
        }

        // Save the result to a new file (preserves the original target PDF)
        targetDoc.Save(outputPdfPath);

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Recursively clones an OutlineItemCollection from one document to another.
    /// </summary>
    private static OutlineItemCollection CloneOutlineItem(OutlineItemCollection source, Document targetDoc)
    {
        // Create a new outline item attached to the target document's outline collection
        var copy = new OutlineItemCollection(targetDoc.Outlines)
        {
            Title = source.Title,
            Italic = source.Italic,
            Bold = source.Bold,
            Color = source.Color,
            Action = source.Action // For simple GoTo actions this works; complex actions may need adaptation
        };

        // Recursively clone any child bookmarks
        foreach (OutlineItemCollection child in source)
        {
            copy.Add(CloneOutlineItem(child, targetDoc));
        }

        return copy;
    }
}
