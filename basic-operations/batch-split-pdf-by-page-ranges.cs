using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string configPath   = "config.txt";         // page ranges, e.g. 1-3
        const string outputDir    = "Sections";           // folder for split PDFs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Read page range definitions
        string[] lines = File.ReadAllLines(configPath);
        var ranges = new List<(int start, int end)>();

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line)) continue; // skip empty lines

            // Expected formats: "5" or "2-7"
            string[] parts = line.Split('-');
            if (parts.Length == 1 && int.TryParse(parts[0], out int singlePage))
            {
                ranges.Add((singlePage, singlePage));
            }
            else if (parts.Length == 2 &&
                     int.TryParse(parts[0], out int startPage) &&
                     int.TryParse(parts[1], out int endPage))
            {
                ranges.Add((startPage, endPage));
            }
            else
            {
                Console.Error.WriteLine($"Invalid range format: '{line}' – skipped.");
            }
        }

        if (ranges.Count == 0)
        {
            Console.Error.WriteLine("No valid page ranges found in configuration.");
            return;
        }

        // Load source PDF once
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            int totalPages = sourceDoc.Pages.Count; // 1‑based count

            int sectionIndex = 1;
            foreach (var (start, end) in ranges)
            {
                // Validate range against source document
                if (start < 1 || end > totalPages || start > end)
                {
                    Console.Error.WriteLine($"Range {start}-{end} is out of bounds – skipped.");
                    continue;
                }

                // Create a new document for this section
                using (Document sectionDoc = new Document())
                {
                    // Copy pages from source to the new document
                    for (int pageNum = start; pageNum <= end; pageNum++) // 1‑based indexing
                    {
                        sectionDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                    }

                    // Build output file name, e.g. Section_1_3.pdf
                    string outFileName = Path.Combine(
                        outputDir,
                        $"Section_{start}_{end}.pdf");

                    // Save the split section (PDF format, no SaveOptions needed)
                    sectionDoc.Save(outFileName);
                    Console.WriteLine($"Saved pages {start}-{end} to '{outFileName}'.");
                }

                sectionIndex++;
            }
        }

        Console.WriteLine("Batch splitting completed.");
    }
}