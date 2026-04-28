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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (using block ensures proper disposal)
            using (Document src = new Document(inputPath))
            {
                int totalPages = src.Pages.Count;
                int splitPage = 10; // split after this page (1‑based)

                // ---------- First part: pages 1 .. splitPage ----------
                using (Document part1 = new Document())
                {
                    int lastPagePart1 = Math.Min(splitPage, totalPages);
                    for (int i = 1; i <= lastPagePart1; i++)
                    {
                        // Add each page to the new document
                        part1.Pages.Add(src.Pages[i]);
                    }

                    string part1Path = Path.Combine(outputDir, $"part1_pages_1-{lastPagePart1}.pdf");
                    part1.Save(part1Path); // Save as PDF
                    Console.WriteLine($"Saved first part: {part1Path}");
                }

                // ---------- Second part: pages splitPage+1 .. end ----------
                if (totalPages > splitPage)
                {
                    using (Document part2 = new Document())
                    {
                        for (int i = splitPage + 1; i <= totalPages; i++)
                        {
                            part2.Pages.Add(src.Pages[i]);
                        }

                        string part2Path = Path.Combine(outputDir, $"part2_pages_{splitPage + 1}-end.pdf");
                        part2.Save(part2Path); // Save as PDF
                        Console.WriteLine($"Saved second part: {part2Path}");
                    }
                }
                else
                {
                    Console.WriteLine("Document has no pages beyond the split point.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}