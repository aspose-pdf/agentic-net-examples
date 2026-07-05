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

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Preserve original page count
            int totalPages = srcDoc.Pages.Count;

            // Separate odd and even pages
            List<Page> oddPages  = new List<Page>();
            List<Page> evenPages = new List<Page>();

            for (int i = 1; i <= totalPages; i++) // 1‑based indexing
            {
                Page page = srcDoc.Pages[i];
                if (i % 2 == 1)
                    oddPages.Add(page);
                else
                    evenPages.Add(page);
            }

            // Create a new document and add pages in the required order
            using (Document targetDoc = new Document())
            {
                // Add odd‑numbered pages first
                foreach (Page p in oddPages)
                    targetDoc.Pages.Add(p); // moves the page from srcDoc to targetDoc

                // Then add even‑numbered pages
                foreach (Page p in evenPages)
                    targetDoc.Pages.Add(p); // moves the page from srcDoc to targetDoc

                // Save the reordered PDF
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}