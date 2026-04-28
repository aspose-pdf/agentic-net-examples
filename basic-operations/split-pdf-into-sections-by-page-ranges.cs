using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string configPath   = "ranges.txt";              // each line: start-end (e.g., 1-3)

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

        // Read and parse page ranges (1‑based, inclusive)
        var ranges = File.ReadAllLines(configPath);
        int sectionIndex = 1;

        try
        {
            // Load the source document once
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                foreach (string line in ranges)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Expected format: start-end (e.g., "5-7")
                    string[] parts = line.Split('-');
                    if (parts.Length != 2 ||
                        !int.TryParse(parts[0].Trim(), out int startPage) ||
                        !int.TryParse(parts[1].Trim(), out int endPage))
                    {
                        Console.Error.WriteLine($"Invalid range line: \"{line}\" – skipped.");
                        continue;
                    }

                    // Validate range against source page count
                    if (startPage < 1 || endPage > sourceDoc.Pages.Count || startPage > endPage)
                    {
                        Console.Error.WriteLine($"Range {startPage}-{endPage} is out of bounds – skipped.");
                        continue;
                    }

                    // Create a new document for this section
                    using (Document sectionDoc = new Document())
                    {
                        // Copy pages from source to the new document (1‑based indexing)
                        for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                        {
                            sectionDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                        }

                        // Save the section PDF
                        string outputPath = $"section_{sectionIndex}_{startPage}_{endPage}.pdf";
                        sectionDoc.Save(outputPath); // PDF output (no SaveOptions needed)
                        Console.WriteLine($"Saved section {sectionIndex}: {outputPath}");
                    }

                    sectionIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}