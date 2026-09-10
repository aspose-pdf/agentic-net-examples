using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";   // Path to the first source PDF
        const string secondPdf = "second.pdf"; // Path to the second source PDF
        const string outputPdf = "merged.pdf"; // Path for the merged result

        // Verify that source files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            // Load both documents. The first document will receive the pages and bookmarks of the second.
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // ----- Merge pages -----
                foreach (Page page in doc2.Pages)
                {
                    // Adding the page object directly preserves its content.
                    doc1.Pages.Add(page);
                }

                // ----- Merge bookmarks/outlines -----
                foreach (OutlineItemCollection outline in doc2.Outlines)
                {
                    // Clone each top‑level outline from the second document and add it to the first.
                    OutlineItemCollection cloned = CloneOutline(outline, doc1);
                    doc1.Outlines.Add(cloned);
                }

                // Save the merged document.
                doc1.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively clones an OutlineItemCollection (bookmark) from a source document
    /// into the target document, preserving title, action, appearance and child hierarchy.
    /// </summary>
    private static OutlineItemCollection CloneOutline(OutlineItemCollection source, Document targetDoc)
    {
        // The constructor requires a parent collection; we use the root of the target document.
        var copy = new OutlineItemCollection(targetDoc.Outlines)
        {
            Title = source.Title,
            Open = source.Open,
            Action = source.Action,
            Color = source.Color,
            Italic = source.Italic,
            Bold = source.Bold
        };

        // Recursively copy child outlines.
        foreach (OutlineItemCollection child in source)
        {
            copy.Add(CloneOutline(child, targetDoc));
        }

        return copy;
    }
}
