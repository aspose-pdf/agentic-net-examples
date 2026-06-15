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

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Collect even‑numbered pages in their original order
            List<Page> evenPages = new List<Page>();
            for (int i = 2; i <= pageCount; i += 2)
            {
                evenPages.Add(doc.Pages[i]);
            }

            // Delete even pages starting from the highest index to avoid shifting
            for (int i = pageCount; i >= 2; i -= 2)
            {
                doc.Pages.Delete(i);
            }

            // Append the previously collected even pages to the end, preserving order
            foreach (Page p in evenPages)
            {
                doc.Pages.Add(p);
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even pages moved to the end. Saved as '{outputPath}'.");
    }
}