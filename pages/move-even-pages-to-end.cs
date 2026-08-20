using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Gather the numbers of all even‑indexed pages (1‑based indexing)
            List<int> evenPageNumbers = new List<int>();
            for (int i = 2; i <= totalPages; i += 2)
                evenPageNumbers.Add(i);

            // Store references to the even pages in their original order
            List<Page> evenPages = new List<Page>();
            foreach (int pageNum in evenPageNumbers)
                evenPages.Add(doc.Pages[pageNum]);

            // Delete the even pages starting from the highest index to avoid re‑indexing issues
            for (int i = evenPageNumbers.Count - 1; i >= 0; i--)
                doc.Pages.Delete(evenPageNumbers[i]);

            // Append the previously collected even pages to the end of the document
            foreach (Page page in evenPages)
                doc.Pages.Add(page);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even pages moved to the end. Output saved to '{outputPath}'.");
    }
}