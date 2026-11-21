using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Needed for bookmark actions (e.g., GoToAction)

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all source files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // The first document becomes the target; its bookmarks are already present
        using (Document target = new Document(inputFiles[0]))
        {
            // Merge remaining documents one by one
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document source = new Document(inputFiles[i]))
                {
                    // Append all pages from the source document
                    target.Pages.Add(source.Pages);

                    // Preserve the source document's bookmarks/outlines
                    // Copy each outline entry (and its children) into the target document
                    foreach (OutlineItemCollection srcOutline in source.Outlines)
                    {
                        // Pass null as the parent for top‑level items; the method will add them to target.Outlines
                        CopyOutline(srcOutline, null, target);
                    }
                }
            }

            // Save the combined PDF; bookmarks from all sources are now included
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }

    /// <summary>
    /// Recursively copies an outline (bookmark) from a source document into the target document.
    /// </summary>
    /// <param name="srcItem">The source outline item to copy.</param>
    /// <param name="targetParent">
    /// The collection (parent item) in the target document where the new item will be added.
    /// Pass null for top‑level items – they will be added to the document's root outlines.
    /// </param>
    /// <param name="targetDoc">The target Document instance (needed for constructing new OutlineItemCollection objects).</param>
    private static void CopyOutline(OutlineItemCollection srcItem, OutlineItemCollection targetParent, Document targetDoc)
    {
        // Create a new outline item for the target document and copy basic properties
        var newItem = new OutlineItemCollection(targetDoc.Outlines)
        {
            Title = srcItem.Title,
            Open = srcItem.Open,
            Italic = srcItem.Italic,
            Bold = srcItem.Bold,
            Color = srcItem.Color,
            Destination = srcItem.Destination // IAppointment can be assigned directly
        };

        // Add the newly created item to the appropriate parent collection in the target document
        if (targetParent == null)
        {
            // Top‑level bookmark – add to the document's root outline collection
            targetDoc.Outlines.Add(newItem);
        }
        else
        {
            // Nested bookmark – add to the parent item
            targetParent.Add(newItem);
        }

        // Recursively copy any child outlines (sub‑bookmarks)
        foreach (OutlineItemCollection child in srcItem)
        {
            CopyOutline(child, newItem, targetDoc);
        }
    }
}
