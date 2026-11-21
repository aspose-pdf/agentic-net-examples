using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string part1Path = "part1_pages_1-10.pdf";
        const string part2Path = "part2_pages_11-end.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document src = new Document(inputPdf))
            {
                int totalPages = src.Pages.Count;

                // ---------- Part 1: pages 1 through 10 ----------
                using (Document part1 = new Document())
                {
                    // Ensure we don't exceed the total page count.
                    int lastPagePart1 = Math.Min(10, totalPages);
                    for (int i = 1; i <= lastPagePart1; i++) // 1‑based indexing
                    {
                        part1.Pages.Add(src.Pages[i]);
                    }
                    part1.Save(part1Path); // Save inside the using scope.
                }

                // ---------- Part 2: pages 11 through end ----------
                using (Document part2 = new Document())
                {
                    // Start from page 11 if it exists.
                    for (int i = 11; i <= totalPages; i++) // 1‑based indexing
                    {
                        part2.Pages.Add(src.Pages[i]);
                    }
                    part2.Save(part2Path);
                }
            }

            Console.WriteLine($"Split completed. Parts saved as '{part1Path}' and '{part2Path}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}