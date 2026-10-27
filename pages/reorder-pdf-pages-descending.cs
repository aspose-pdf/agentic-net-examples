using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sorted_desc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document srcDoc = new Document(inputPath))
        {
            // Collect each page together with its original 1‑based index
            List<(int Index, Page Page)> pages = new List<(int, Page)>();
            for (int i = 1; i <= srcDoc.Pages.Count; i++) // page-indexing-one-based rule
            {
                pages.Add((i, srcDoc.Pages[i]));
            }

            // Custom sort: descending numeric order based on the original index
            pages.Sort((a, b) => b.Index.CompareTo(a.Index));

            // Create a new empty document to hold the reordered pages
            using (Document targetDoc = new Document())
            {
                // Append pages to the target document in the sorted order
                foreach (var entry in pages)
                {
                    // Adding a page copies it into the target document
                    targetDoc.Pages.Add(entry.Page);
                }

                // Save the reordered PDF (PDF format, no special SaveOptions needed)
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages reordered in descending order and saved to '{outputPath}'.");
    }
}