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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Collect references to all even‑numbered pages (1‑based indexing)
            List<Page> evenPages = new List<Page>();
            for (int i = 2; i <= doc.Pages.Count; i += 2)
            {
                evenPages.Add(doc.Pages[i]);
            }

            // Delete even pages starting from the highest index to avoid re‑indexing issues
            for (int i = doc.Pages.Count; i >= 2; i--)
            {
                if (i % 2 == 0)
                {
                    doc.Pages.Delete(i);
                }
            }

            // Append the previously collected even pages to the end, preserving order
            foreach (Page page in evenPages)
            {
                doc.Pages.Add(page);
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}