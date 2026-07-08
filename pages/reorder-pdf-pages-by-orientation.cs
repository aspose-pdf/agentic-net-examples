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

        // Load source PDF (wrapped in using for deterministic disposal)
        using (Document srcDoc = new Document(inputPath))
        {
            // Create an empty target PDF
            using (Document targetDoc = new Document())
            {
                // Separate pages into landscape and portrait collections
                List<Page> landscapePages = new List<Page>();
                List<Page> portraitPages  = new List<Page>();

                for (int i = 1; i <= srcDoc.Pages.Count; i++) // 1‑based indexing
                {
                    Page page = srcDoc.Pages[i];
                    // PageInfo.IsLandscape indicates orientation
                    if (page.PageInfo != null && page.PageInfo.IsLandscape)
                        landscapePages.Add(page);
                    else
                        portraitPages.Add(page);
                }

                // Add landscape pages first
                foreach (Page p in landscapePages)
                    targetDoc.Pages.Add(p);

                // Followed by portrait pages
                foreach (Page p in portraitPages)
                    targetDoc.Pages.Add(p);

                // Save the reordered PDF
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}