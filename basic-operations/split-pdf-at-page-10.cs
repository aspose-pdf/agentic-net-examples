using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Build descriptive output filenames based on the source name
        string baseName   = Path.GetFileNameWithoutExtension(inputPath);
        string part1Path  = $"{baseName}_part1.pdf";
        string part2Path  = $"{baseName}_part2.pdf";

        // Load the source PDF (using block ensures proper disposal)
        using (Document src = new Document(inputPath))
        {
            int totalPages = src.Pages.Count;

            // Ensure there are at least 10 pages to split at
            if (totalPages < 10)
            {
                Console.Error.WriteLine("Document has fewer than 10 pages; cannot split as requested.");
                return;
            }

            // ---------- First part: pages 1 through 10 ----------
            using (Document part1 = new Document())
            {
                for (int i = 1; i <= 10; i++)               // 1‑based indexing
                {
                    part1.Pages.Add(src.Pages[i]);           // copy page to new document
                }
                part1.Save(part1Path);                       // save as PDF
            }

            // ---------- Second part: pages 11 through end ----------
            using (Document part2 = new Document())
            {
                for (int i = 11; i <= totalPages; i++)       // continue from page 11
                {
                    part2.Pages.Add(src.Pages[i]);           // copy page to new document
                }
                part2.Save(part2Path);                       // save as PDF
            }
        }

        Console.WriteLine($"First part saved to '{part1Path}'.");
        Console.WriteLine($"Second part saved to '{part2Path}'.");
    }
}