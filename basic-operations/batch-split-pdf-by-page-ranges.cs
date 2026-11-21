using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string configPath = "ranges.txt";
        const string outputDir = "Sections";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Parse page ranges from configuration file.
        // Expected format per line: start-end (both inclusive), e.g., "1-5"
        List<(int start, int end)> ranges = new List<(int, int)>();
        foreach (string line in File.ReadAllLines(configPath))
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;

            string[] parts = trimmed.Split('-');
            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int start) ||
                !int.TryParse(parts[1], out int end) ||
                start < 1 || end < start)
            {
                Console.Error.WriteLine($"Invalid range line: '{line}'");
                continue;
            }

            ranges.Add((start, end));
        }

        if (ranges.Count == 0)
        {
            Console.Error.WriteLine("No valid page ranges found.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open source PDF once using a using block (document-disposal-with-using rule)
        using (Document srcDoc = new Document(inputPdf))
        {
            int totalPages = srcDoc.Pages.Count; // page-indexing-one-based rule

            foreach (var (start, end) in ranges)
            {
                // Skip ranges that exceed the source document page count
                if (end > totalPages)
                {
                    Console.Error.WriteLine($"Range {start}-{end} exceeds total pages ({totalPages}). Skipping.");
                    continue;
                }

                // Create a new document for the current section
                using (Document sectionDoc = new Document())
                {
                    // Add pages from source to the new document (1‑based indexing)
                    for (int i = start; i <= end; i++)
                    {
                        sectionDoc.Pages.Add(srcDoc.Pages[i]);
                    }

                    // Build output file name, e.g., "section_1-5.pdf"
                    string outPath = Path.Combine(outputDir, $"section_{start}-{end}.pdf");

                    // Save the section (PDF format by default)
                    sectionDoc.Save(outPath);
                    Console.WriteLine($"Saved pages {start}-{end} to '{outPath}'.");
                }
            }
        }
    }
}