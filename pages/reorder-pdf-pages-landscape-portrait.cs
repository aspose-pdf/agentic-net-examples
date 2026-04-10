using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (lifecycle rule: load)
        using (Document src = new Document(inputPath))
        {
            // Separate pages into landscape and portrait collections
            List<Page> landscapePages = new List<Page>();
            List<Page> portraitPages  = new List<Page>();

            // Aspose.Pdf uses 1‑based page indexing (global rule)
            for (int i = 1; i <= src.Pages.Count; i++)
            {
                Page page = src.Pages[i];

                // Determine orientation using PageInfo.IsLandscape (the correct property)
                bool isLandscape = page.PageInfo.IsLandscape;

                if (isLandscape)
                    landscapePages.Add(page);
                else
                    portraitPages.Add(page);
            }

            // Create a new empty PDF document (lifecycle rule: create)
            using (Document dest = new Document())
            {
                // Add landscape pages first
                foreach (Page p in landscapePages)
                {
                    dest.Pages.Add(p);
                }

                // Follow with portrait pages
                foreach (Page p in portraitPages)
                {
                    dest.Pages.Add(p);
                }

                // Save the reordered PDF (lifecycle rule: save)
                dest.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}
