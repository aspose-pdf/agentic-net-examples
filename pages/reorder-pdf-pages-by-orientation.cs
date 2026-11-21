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

        // Load source PDF and create an empty target PDF
        using (Document src = new Document(inputPath))
        using (Document dst = new Document())
        {
            // Separate pages by orientation
            List<Page> landscapePages = new List<Page>();
            List<Page> portraitPages  = new List<Page>();

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= src.Pages.Count; i++)
            {
                Page page = src.Pages[i];
                // Width > Height => landscape, otherwise portrait
                if (page.PageInfo.Width > page.PageInfo.Height)
                    landscapePages.Add(page);
                else
                    portraitPages.Add(page);
            }

            // Add landscape pages first
            foreach (Page page in landscapePages)
                dst.Pages.Add(page);

            // Then add portrait pages
            foreach (Page page in portraitPages)
                dst.Pages.Add(page);

            // Save the reordered document
            dst.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}