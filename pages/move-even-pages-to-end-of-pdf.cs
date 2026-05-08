using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Collect references to even‑numbered pages (1‑based indexing)
            List<Page> evenPages = new List<Page>();
            for (int i = 2; i <= doc.Pages.Count; i += 2)
            {
                evenPages.Add(doc.Pages[i]); // page at position i
            }

            // Delete even pages in reverse order to avoid index shift
            for (int i = (doc.Pages.Count / 2) * 2; i >= 2; i -= 2)
            {
                doc.Pages.Delete(i);
            }

            // Append the previously collected even pages to the end,
            // preserving their original relative order
            foreach (Page page in evenPages)
            {
                doc.Pages.Add(page);
            }

            // Save the modified document (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}