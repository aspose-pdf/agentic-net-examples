using System;
using System.Collections.Generic;
using System.IO;
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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Separate pages into landscape and portrait collections
            List<Page> landscapePages = new List<Page>();
            List<Page> portraitPages = new List<Page>();

            // Pages are 1‑based (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // PageInfo.IsLandscape indicates orientation
                if (page.PageInfo != null && page.PageInfo.IsLandscape)
                    landscapePages.Add(page);
                else
                    portraitPages.Add(page);
            }

            // Remove all existing pages (clears the collection)
            doc.Pages.Delete();

            // Add landscape pages first, then portrait pages (Add(Page) method)
            foreach (Page p in landscapePages)
                doc.Pages.Add(p);

            foreach (Page p in portraitPages)
                doc.Pages.Add(p);

            // Save the reordered PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}