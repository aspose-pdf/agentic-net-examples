using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document src = new Document(inputPath))
        {
            // Separate pages into landscape and portrait collections
            List<Page> landscapePages = new List<Page>();
            List<Page> portraitPages = new List<Page>();

            for (int i = 1; i <= src.Pages.Count; i++) // 1‑based indexing
            {
                Page page = src.Pages[i];
                // PageInfo.IsLandscape indicates orientation
                if (page.PageInfo != null && page.PageInfo.IsLandscape)
                    landscapePages.Add(page);
                else
                    portraitPages.Add(page);
            }

            // Create a new PDF and add pages in the desired order
            using (Document dest = new Document())
            {
                foreach (Page p in landscapePages)
                {
                    dest.Pages.Add(p);
                }

                foreach (Page p in portraitPages)
                {
                    dest.Pages.Add(p);
                }

                // Save the reordered document
                dest.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}