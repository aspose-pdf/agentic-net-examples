using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered_descending.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Target document must stay alive until Save() completes.
        using (Document target = new Document())
        // Source document containing the original pages.
        using (Document source = new Document(inputPath))
        {
            // Build a list of page numbers (1‑based) and sort it descending
            // using a custom comparer (lambda) – this is the custom sorting algorithm.
            List<int> pageIndices = Enumerable.Range(1, source.Pages.Count).ToList();
            pageIndices.Sort((a, b) => b.CompareTo(a)); // descending order

            // Add pages to the target document following the sorted order.
            foreach (int idx in pageIndices)
            {
                // Pages are 1‑based; source.Pages[idx] returns the original page.
                target.Pages.Add(source.Pages[idx]);
            }

            // Save the reordered PDF.
            target.Save(outputPath);
        }

        Console.WriteLine($"Pages reordered in descending order and saved to '{outputPath}'.");
    }
}