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

        // Each line in the config file should be "start-end" (1‑based, inclusive)
        var ranges = new List<(int start, int end)>();
        foreach (var line in File.ReadAllLines(configPath))
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;
            var parts = trimmed.Split('-');
            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int s) ||
                !int.TryParse(parts[1], out int e) ||
                s < 1 || e < s)
            {
                Console.Error.WriteLine($"Invalid range: '{line}'");
                continue;
            }
            ranges.Add((s, e));
        }

        if (ranges.Count == 0)
        {
            Console.WriteLine("No valid page ranges found in configuration.");
            return;
        }

        using (Document source = new Document(inputPdf))
        {
            int partIndex = 1;
            foreach (var (start, end) in ranges)
            {
                if (start > source.Pages.Count)
                {
                    Console.WriteLine($"Range {start}-{end} starts after the last page; skipping.");
                    continue;
                }
                int actualEnd = Math.Min(end, source.Pages.Count);

                using (Document part = new Document())
                {
                    for (int i = start; i <= actualEnd; i++)
                    {
                        part.Pages.Add(source.Pages[i]); // 1‑based indexing
                    }

                    string outPath = $"output_part_{partIndex}.pdf";
                    part.Save(outPath);
                    Console.WriteLine($"Saved pages {start}-{actualEnd} to {outPath}");
                }

                partIndex++;
            }
        }
    }
}