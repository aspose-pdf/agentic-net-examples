using System;
using System.IO;
using System.Collections.Generic;
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

        // Load source PDF inside a using block (ensures disposal)
        using (Document src = new Document(inputPath))
        {
            // Separate pages into landscape and portrait collections
            List<Page> landscapePages = new List<Page>();
            List<Page> portraitPages  = new List<Page>();

            // Pages are 1‑based indexed
            for (int i = 1; i <= src.Pages.Count; i++)
            {
                Page page = src.Pages[i];

                // PageInfo.IsLandscape indicates orientation
                if (page.PageInfo != null && page.PageInfo.IsLandscape)
                    landscapePages.Add(page);
                else
                    portraitPages.Add(page);
            }

            // Create a new empty document for the reordered result
            using (Document dst = new Document())
            {
                // First add all landscape pages
                foreach (Page p in landscapePages)
                {
                    dst.Pages.Add(p);
                }

                // Then add all portrait pages
                foreach (Page p in portraitPages)
                {
                    dst.Pages.Add(p);
                }

                // Save the reordered PDF
                dst.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}