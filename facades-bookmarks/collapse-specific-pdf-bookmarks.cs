using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Titles of the bookmarks that should appear collapsed when the PDF is opened
        string[] bookmarksToCollapse = { "Chapter 1", "Appendix" };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the document model (not the facade)
        Document pdfDocument = new Document(inputPath);

        // Collapse the specified bookmarks by setting their Open property to false
        SetBookmarksOpenState(pdfDocument.Outlines, bookmarksToCollapse, false);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Bookmarks collapsed and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Recursively sets the Open state of outline items whose titles match any of the supplied titles.
    /// </summary>
    /// <param name="collection">The outline collection (or any outline item that is itself enumerable) to walk.</param>
    /// <param name="titles">Array of bookmark titles to affect.</param>
    /// <param name="openState">Desired open state (false = collapsed).</param>
    private static void SetBookmarksOpenState(IEnumerable<OutlineItemCollection> collection, string[] titles, bool openState)
    {
        foreach (OutlineItemCollection item in collection)
        {
            // If the title matches one of the requested bookmarks, set its Open flag.
            if (Array.Exists(titles, t => string.Equals(t, item.Title, StringComparison.OrdinalIgnoreCase)))
            {
                item.Open = openState;
            }

            // Recurse into child outlines, if any. An OutlineItemCollection is itself enumerable of its children.
            if (item != null && item.GetEnumerator().MoveNext())
            {
                SetBookmarksOpenState(item, titles, openState);
            }
        }
    }
}
