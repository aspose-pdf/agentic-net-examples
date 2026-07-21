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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Collect even‑numbered pages (1‑based indexing) and their numbers
            List<int> evenPageNumbers = new List<int>();
            List<Page> evenPages = new List<Page>();

            for (int i = 2; i <= pageCount; i += 2)
            {
                evenPageNumbers.Add(i);
                evenPages.Add(doc.Pages[i]); // capture the Page object
            }

            // Remove the even pages from their original positions
            doc.Pages.Delete(evenPageNumbers.ToArray());

            // Append the captured even pages to the end, preserving order
            doc.Pages.Add(evenPages.ToArray());

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even pages moved to the end. Saved as '{outputPath}'.");
    }
}