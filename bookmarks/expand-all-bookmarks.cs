using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "expanded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Recursively set the Open property of every bookmark (outline item) to true
            ExpandAllOutlines(doc.Outlines);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks expanded and saved to '{outputPath}'.");
    }

    // Helper method to traverse the outline tree. Both OutlineCollection (doc.Outlines)
    // and OutlineItemCollection implement IEnumerable<OutlineItemCollection>, so we can
    // accept any enumerable of OutlineItemCollection.
    private static void ExpandAllOutlines(IEnumerable<OutlineItemCollection> outlines)
    {
        foreach (var item in outlines)
        {
            // Ensure the bookmark is expanded
            item.Open = true;

            // If the bookmark has child items, process them recursively
            if (item.Count > 0)
            {
                ExpandAllOutlines(item);
            }
        }
    }
}
