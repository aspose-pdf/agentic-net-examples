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

        // Load source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create an empty target PDF
            using (Document targetDoc = new Document())
            {
                List<int> landscapePages = new List<int>();
                List<int> portraitPages  = new List<int>();

                // Classify pages (1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page page = srcDoc.Pages[i];
                    if (page.PageInfo.IsLandscape)
                        landscapePages.Add(i);
                    else
                        portraitPages.Add(i);
                }

                // Append landscape pages first
                foreach (int idx in landscapePages)
                {
                    // Add moves the page from srcDoc to targetDoc
                    targetDoc.Pages.Add(srcDoc.Pages[idx]);
                }

                // Then append portrait pages
                foreach (int idx in portraitPages)
                {
                    targetDoc.Pages.Add(srcDoc.Pages[idx]);
                }

                // Save the reordered PDF
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}