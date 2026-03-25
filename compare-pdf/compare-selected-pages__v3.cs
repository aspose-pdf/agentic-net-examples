using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "doc1.pdf";
        const string file2 = "doc2.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(file1))
        using (Document doc2 = new Document(file2))
        {
            // Define the pages you want to compare (1‑based indexing)
            int[] pagesToCompare = { 1, 2, 4 };

            // Comparison options – default settings are sufficient for a basic text comparison
            ComparisonOptions options = new ComparisonOptions();

            foreach (int pageNum in pagesToCompare)
            {
                // Ensure the requested page exists in both documents
                if (pageNum > doc1.Pages.Count || pageNum > doc2.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNum} is out of range – skipping.");
                    continue;
                }

                Page page1 = doc1.Pages[pageNum];
                Page page2 = doc2.Pages[pageNum];

                // Compare the two pages and obtain a list of differences
                List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);

                Console.WriteLine($"Page {pageNum}: {diffs.Count} difference(s) found.");
                // Additional processing of diffs can be added here if needed
            }
        }
    }
}