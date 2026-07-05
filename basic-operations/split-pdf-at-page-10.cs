using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document sourceDoc = new Document(inputPath))
            {
                int totalPages = sourceDoc.Pages.Count;
                int splitPage = 10; // split after this page

                // Ensure the split page is within the document range
                if (splitPage > totalPages)
                {
                    Console.Error.WriteLine($"Document has only {totalPages} pages; cannot split at page {splitPage}.");
                    return;
                }

                // ----- Part 1: pages 1 .. splitPage -----
                using (Document part1 = new Document())
                {
                    for (int i = 1; i <= splitPage; i++)
                    {
                        part1.Pages.Add(sourceDoc.Pages[i]);
                    }

                    string part1Path = $"output_pages_1_to_{splitPage}.pdf";
                    part1.Save(part1Path);
                    Console.WriteLine($"Saved first part to '{part1Path}'.");
                }

                // ----- Part 2: pages (splitPage + 1) .. end -----
                using (Document part2 = new Document())
                {
                    for (int i = splitPage + 1; i <= totalPages; i++)
                    {
                        part2.Pages.Add(sourceDoc.Pages[i]);
                    }

                    string part2Path = $"output_pages_{splitPage + 1}_to_{totalPages}.pdf";
                    part2.Save(part2Path);
                    Console.WriteLine($"Saved second part to '{part2Path}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}