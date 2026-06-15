using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string configPath = "ranges.txt";     // configuration file with page ranges
        const string outputDir  = "Sections";       // folder for split PDFs

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Parse configuration file.
        // Expected line format: SectionName:startPage-endPage
        // Example: Chapter1:1-5
        var sections = new List<(string Name, int Start, int End)>();
        foreach (var line in File.ReadAllLines(configPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue; // skip empty lines and comments

            var parts = line.Split(':');
            if (parts.Length != 2)
                continue; // malformed line

            var name = parts[0].Trim();
            var range = parts[1].Trim();
            var bounds = range.Split('-');
            if (bounds.Length != 2)
                continue; // malformed range

            if (int.TryParse(bounds[0], out int start) && int.TryParse(bounds[1], out int end))
            {
                // Ensure start <= end
                if (start > end) (start, end) = (end, start);
                sections.Add((name, start, end));
            }
        }

        if (sections.Count == 0)
        {
            Console.Error.WriteLine("No valid sections found in the configuration file.");
            return;
        }

        // Load the source PDF once
        using (Document source = new Document(inputPdf))
        {
            int totalPages = source.Pages.Count; // 1‑based page count

            foreach (var (name, start, end) in sections)
            {
                // Clamp the requested range to the actual page count
                int from = Math.Max(1, start);
                int to   = Math.Min(totalPages, end);
                if (from > to)
                {
                    Console.WriteLine($"Skipping section '{name}' – range out of bounds.");
                    continue;
                }

                // Create a new document for this section
                using (Document part = new Document())
                {
                    // Copy pages from the source document (pages are 1‑based)
                    for (int i = from; i <= to; i++)
                    {
                        part.Pages.Add(source.Pages[i]);
                    }

                    // Save the split section as a separate PDF
                    string outPath = Path.Combine(outputDir, $"{name}.pdf");
                    part.Save(outPath);
                    Console.WriteLine($"Saved section '{name}' ({from}-{to}) to '{outPath}'.");
                }
            }
        }
    }
}