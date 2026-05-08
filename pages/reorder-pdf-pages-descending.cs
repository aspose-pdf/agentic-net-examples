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
        const string outputPath = "sorted_descending.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Extract pages into a list for custom sorting
            List<Page> pageList = srcDoc.Pages.Cast<Page>().ToList();

            // Sort pages in descending order based on their original position
            // (IndexOf returns 1‑based page numbers)
            pageList = pageList
                .OrderByDescending(p => srcDoc.Pages.IndexOf(p))
                .ToList();

            // Create a new empty document to hold the reordered pages
            using (Document destDoc = new Document())
            {
                // Add pages to the new document in the sorted order
                foreach (Page pg in pageList)
                {
                    destDoc.Pages.Add(pg);
                }

                // Optional: reorganize the page tree for better performance
                destDoc.PageNodesToBalancedTree();

                // Save the reordered PDF
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Pages reordered and saved to '{outputPath}'.");
    }
}