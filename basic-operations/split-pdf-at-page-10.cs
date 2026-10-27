using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitParts";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (1‑based page indexing)
            using (Document src = new Document(inputPdf))
            {
                int totalPages = src.Pages.Count;
                int splitPage   = 10; // split after this page

                // ---------- Part 1: pages 1 … splitPage ----------
                using (Document part1 = new Document())
                {
                    // Add pages 1 through splitPage (or up to totalPages if fewer)
                    for (int i = 1; i <= Math.Min(splitPage, totalPages); i++)
                    {
                        part1.Pages.Add(src.Pages[i]);
                    }

                    string part1Path = Path.Combine(outputDir, $"part1_pages_1-{Math.Min(splitPage, totalPages)}.pdf");
                    part1.Save(part1Path);
                    Console.WriteLine($"Saved part 1 → {part1Path}");
                }

                // ---------- Part 2: pages splitPage+1 … end ----------
                if (totalPages > splitPage)
                {
                    using (Document part2 = new Document())
                    {
                        for (int i = splitPage + 1; i <= totalPages; i++)
                        {
                            part2.Pages.Add(src.Pages[i]);
                        }

                        string part2Path = Path.Combine(outputDir, $"part2_pages_{splitPage + 1}-end.pdf");
                        part2.Save(part2Path);
                        Console.WriteLine($"Saved part 2 → {part2Path}");
                    }
                }
                else
                {
                    Console.WriteLine("Source PDF has fewer pages than the split point; only part 1 was created.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}