using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Create a sample PDF with outlines if it does not already exist
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add a blank page (required for a valid PDF)
                seed.Pages.Add();

                // Create a top‑level outline entry
                OutlineItemCollection top = new OutlineItemCollection(seed.Outlines)
                {
                    Title = "Chapter 1",
                    Open = true
                };
                seed.Outlines.Add(top);

                // Create a child outline entry under the top‑level one
                OutlineItemCollection child = new OutlineItemCollection(seed.Outlines)
                {
                    Title = "Section 1.1",
                    Open = false
                };
                top.Add(child);

                // Save the seed PDF so the later load has a file to read
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            OutlineCollection outlines = doc.Outlines;

            // Verify that the document contains outline entries
            if (outlines == null || outlines.Count == 0)
            {
                Console.WriteLine("No outline entries found in the document.");
                return;
            }

            // Iterate over top‑level outline items and print the hierarchy
            foreach (OutlineItemCollection topItem in outlines)
            {
                PrintOutlineItem(topItem, 0);
            }
        }
    }

    // Recursively prints an outline item and its children with indentation
    static void PrintOutlineItem(OutlineItemCollection item, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}- {item.Title}");

        // Each OutlineItemCollection also acts as a collection of its child items
        foreach (OutlineItemCollection child in item)
        {
            PrintOutlineItem(child, depth + 1);
        }
    }
}