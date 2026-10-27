using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfSplitter
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string configPath   = "ranges.txt"; // each line: start-end (e.g., 1-3)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read and parse page ranges from the configuration file
        var ranges = File.ReadAllLines(configPath);
        int sectionIndex = 1;

        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Validate that the source document has pages
            if (sourceDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Source PDF contains no pages.");
                return;
            }

            foreach (string rawLine in ranges)
            {
                string line = rawLine.Trim();

                // Skip empty lines or comments
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                // Expected format: start-end (both inclusive)
                string[] parts = line.Split('-');
                if (parts.Length != 2 ||
                    !int.TryParse(parts[0], out int startPage) ||
                    !int.TryParse(parts[1], out int endPage))
                {
                    Console.Error.WriteLine($"Invalid range format: '{line}'. Expected 'start-end'.");
                    continue;
                }

                // Ensure 1‑based indexing and valid bounds
                if (startPage < 1 || endPage < startPage || endPage > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Range out of bounds: '{line}'. PDF has {sourceDoc.Pages.Count} pages.");
                    continue;
                }

                // Create a new document for this section
                using (Document sectionDoc = new Document())
                {
                    // Copy pages from source to the new document
                    for (int i = startPage; i <= endPage; i++) // 1‑based page indexing
                    {
                        sectionDoc.Pages.Add(sourceDoc.Pages[i]);
                    }

                    // Build output file name
                    string outputPath = $"section_{sectionIndex}_{startPage}-{endPage}.pdf";

                    // Save the split section
                    sectionDoc.Save(outputPath);
                    Console.WriteLine($"Saved pages {startPage}-{endPage} to '{outputPath}'.");
                }

                sectionIndex++;
            }
        }

        Console.WriteLine("Batch splitting completed.");
    }
}