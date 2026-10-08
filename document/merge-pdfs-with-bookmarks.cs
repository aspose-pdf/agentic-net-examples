using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the target (first) and source (second) documents
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Append all pages from the source document to the target document
            target.Pages.Add(source.Pages);

            // Preserve bookmarks (outlines) from the source document
            foreach (OutlineItemCollection srcOutline in source.Outlines)
            {
                OutlineItemCollection cloned = CloneOutline(srcOutline, target);
                target.Outlines.Add(cloned);
            }

            // Save the merged PDF with bookmarks from both sources
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }

    /// <summary>
    /// Recursively clones an outline item (and its children) from a source document
    /// into the target document.
    /// </summary>
    private static OutlineItemCollection CloneOutline(OutlineItemCollection sourceItem, Document targetDoc)
    {
        // Create a new outline item attached to the target document's root outline collection
        var clonedItem = new OutlineItemCollection(targetDoc.Outlines)
        {
            Title = sourceItem.Title,
            Italic = sourceItem.Italic,
            Bold = sourceItem.Bold,
            Color = sourceItem.Color,
            // The Action object can be reused; if a more complex copy is required,
            // additional handling would be needed.
            Action = sourceItem.Action
        };

        // Recursively clone child outline items
        foreach (OutlineItemCollection child in sourceItem)
        {
            clonedItem.Add(CloneOutline(child, targetDoc));
        }

        return clonedItem;
    }
}
