using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPart1 = "output_part1.pdf";
        const string outputPart2 = "output_part2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document src = new Document(inputPath))
            {
                int totalPages = src.Pages.Count;

                // Ensure there are at least 10 pages to split at page ten
                if (totalPages < 10)
                {
                    Console.Error.WriteLine("The document has fewer than 10 pages; cannot split at page ten.");
                    return;
                }

                // ----- Part 1: pages 1 through 10 -----
                using (Document part1 = new Document())
                {
                    for (int i = 1; i <= 10; i++) // 1‑based indexing
                    {
                        part1.Pages.Add(src.Pages[i]);
                    }
                    part1.Save(outputPart1); // Save as PDF
                }

                // ----- Part 2: pages 11 through the end -----
                using (Document part2 = new Document())
                {
                    for (int i = 11; i <= totalPages; i++) // 1‑based indexing
                    {
                        part2.Pages.Add(src.Pages[i]);
                    }
                    part2.Save(outputPart2); // Save as PDF
                }
            }

            Console.WriteLine($"PDF split completed: '{outputPart1}' and '{outputPart2}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}