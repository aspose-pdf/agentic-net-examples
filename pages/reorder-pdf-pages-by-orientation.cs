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

        // Open source PDF and create an empty target PDF
        using (Document src = new Document(inputPath))
        using (Document dest = new Document())
        {
            // Lists to hold page numbers (1‑based) for each orientation
            List<int> landscapePages = new List<int>();
            List<int> portraitPages  = new List<int>();

            // Determine orientation of each page
            for (int i = 1; i <= src.Pages.Count; i++) // 1‑based indexing
            {
                Page page = src.Pages[i];
                double width  = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                if (width > height)
                    landscapePages.Add(i);
                else
                    portraitPages.Add(i);
            }

            // Add landscape pages first
            foreach (int idx in landscapePages)
                dest.Pages.Add(src.Pages[idx]);

            // Then add portrait pages
            foreach (int idx in portraitPages)
                dest.Pages.Add(src.Pages[idx]);

            // Save the reordered document
            dest.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}