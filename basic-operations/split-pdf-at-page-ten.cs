using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string part1Path = "output_part1_pages_1_to_10.pdf";
        const string part2Path = "output_part2_pages_11_onwards.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document source = new Document(inputPath))
        {
            int totalPages = source.Pages.Count;
            if (totalPages < 10)
            {
                Console.Error.WriteLine("Source PDF has fewer than 10 pages; cannot split as requested.");
                return;
            }

            // Create first part (pages 1‑10)
            using (Document part1 = new Document())
            {
                for (int i = 1; i <= 10; i++)
                {
                    part1.Pages.Add(source.Pages[i]);
                }
                part1.Save(part1Path); // Save first part
            }

            // Create second part (pages 11‑end)
            using (Document part2 = new Document())
            {
                for (int i = 11; i <= totalPages; i++)
                {
                    part2.Pages.Add(source.Pages[i]);
                }
                part2.Save(part2Path); // Save second part
            }
        }

        Console.WriteLine($"PDF split completed:\n  Part 1 → {part1Path}\n  Part 2 → {part2Path}");
    }
}