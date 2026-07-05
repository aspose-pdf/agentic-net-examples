using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Entry point
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string configFilePath = "ranges.txt";         // configuration file with page ranges
        const string outputFolder   = "Sections";          // folder where split PDFs will be saved

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Parse page ranges from configuration file
        List<(int start, int end)> ranges = ParseRanges(configFilePath);
        if (ranges.Count == 0)
        {
            Console.Error.WriteLine("No valid page ranges found in configuration file.");
            return;
        }

        try
        {
            // Load source PDF once
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                int totalPages = sourceDoc.Pages.Count; // 1‑based page count

                foreach (var (start, end) in ranges)
                {
                    // Validate range against source document
                    if (start < 1 || end > totalPages || start > end)
                    {
                        Console.Error.WriteLine($"Invalid range [{start}-{end}] – skipped.");
                        continue;
                    }

                    // Create a new PDF for the current range
                    using (Document targetDoc = new Document())
                    {
                        // Copy pages from source to target (pages are 1‑based)
                        for (int pageNum = start; pageNum <= end; pageNum++)
                        {
                            targetDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                        }

                        // Build output file name, e.g. "section_1_3.pdf"
                        string outputPath = Path.Combine(
                            outputFolder,
                            $"section_{start}_{end}.pdf");

                        // Save the split section
                        targetDoc.Save(outputPath);
                        Console.WriteLine($"Saved pages {start}-{end} → {outputPath}");
                    }
                }
            }

            Console.WriteLine("Batch splitting completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }

    // Reads a text file where each non‑empty line defines a page range:
    //   start-end   (e.g., "1-5")
    // Lines starting with '#' are treated as comments.
    private static List<(int start, int end)> ParseRanges(string filePath)
    {
        var result = new List<(int start, int end)>();
        foreach (string rawLine in File.ReadAllLines(filePath))
        {
            string line = rawLine.Trim();

            // Skip empty lines and comments
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;

            // Expect format "start-end"
            string[] parts = line.Split('-');
            if (parts.Length != 2)
                continue; // malformed line – ignore

            if (int.TryParse(parts[0].Trim(), out int start) &&
                int.TryParse(parts[1].Trim(), out int end))
            {
                result.Add((start, end));
            }
        }
        return result;
    }
}