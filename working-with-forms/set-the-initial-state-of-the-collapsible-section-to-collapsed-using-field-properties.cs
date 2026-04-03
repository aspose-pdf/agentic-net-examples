using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "collapsed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has an outline (bookmarks) collection
            if (doc.Outlines != null && doc.Outlines.Count > 0)
            {
                // Example: collapse the first top‑level outline item
                // OutlineItemCollection.Open controls the initial expanded/collapsed state
                OutlineItemCollection firstItem = doc.Outlines[1]; // 1‑based indexing
                firstItem.Open = false; // Set to collapsed

                // Collapse all outline items recursively
                CollapseAll(doc.Outlines);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with collapsed outline: {outputPath}");
    }

    // Recursively set Open = false for every outline item
    static void CollapseAll(IEnumerable<OutlineItemCollection> collection)
    {
        foreach (OutlineItemCollection item in collection)
        {
            item.Open = false;
            if (item.Count > 0)
                CollapseAll(item);
        }
    }
}
