using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitParts";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document source = new Document(inputPath))
            {
                int totalPages = source.Pages.Count;

                // Validate that the document has at least 10 pages
                if (totalPages < 10)
                {
                    Console.Error.WriteLine("Document has fewer than 10 pages; cannot split at page ten.");
                    return;
                }

                // ---------- Part 1: pages 1‑10 ----------
                using (Document part1 = new Document())
                {
                    for (int i = 1; i <= 10; i++)               // Pages are 1‑based
                    {
                        part1.Pages.Add(source.Pages[i]);
                    }

                    string part1Path = Path.Combine(outputDir, "part1_pages_1-10.pdf");
                    part1.Save(part1Path);                       // Save the first part
                    Console.WriteLine($"Saved first part: {part1Path}");
                }

                // ---------- Part 2: pages 11‑end ----------
                using (Document part2 = new Document())
                {
                    for (int i = 11; i <= totalPages; i++)       // Remaining pages
                    {
                        part2.Pages.Add(source.Pages[i]);
                    }

                    string part2Path = Path.Combine(outputDir, $"part2_pages_11-{totalPages}.pdf");
                    part2.Save(part2Path);                       // Save the second part
                    Console.WriteLine($"Saved second part: {part2Path}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}