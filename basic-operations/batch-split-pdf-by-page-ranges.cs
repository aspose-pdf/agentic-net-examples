using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string configPath   = "ranges.txt";        // each line: start[-end]
        const string outputFolder = "Sections";          // folder for split PDFs

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

        Directory.CreateDirectory(outputFolder);

        // Read all page range definitions
        string[] rangeLines = File.ReadAllLines(configPath);
        int sectionIndex = 1;

        using (Document sourceDoc = new Document(inputPdfPath))
        {
            int totalPages = sourceDoc.Pages.Count; // 1‑based count

            foreach (string rawLine in rangeLines)
            {
                string line = rawLine.Trim();

                // Skip empty or comment lines
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                int startPage, endPage;

                // Parse "start-end" or single "page"
                string[] parts = line.Split('-');
                if (parts.Length == 1)
                {
                    // Single page
                    if (!int.TryParse(parts[0], out startPage))
                    {
                        Console.Error.WriteLine($"Invalid page number: '{line}'");
                        continue;
                    }
                    endPage = startPage;
                }
                else if (parts.Length == 2)
                {
                    if (!int.TryParse(parts[0], out startPage) ||
                        !int.TryParse(parts[1], out endPage))
                    {
                        Console.Error.WriteLine($"Invalid page range: '{line}'");
                        continue;
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Malformed line: '{line}'");
                    continue;
                }

                // Validate range against source document
                if (startPage < 1 || endPage > totalPages || startPage > endPage)
                {
                    Console.Error.WriteLine($"Range out of bounds: {startPage}-{endPage}");
                    continue;
                }

                // Create a new PDF for this section
                using (Document sectionDoc = new Document())
                {
                    // Copy pages from source to the new document
                    for (int i = startPage; i <= endPage; i++)
                    {
                        sectionDoc.Pages.Add(sourceDoc.Pages[i]); // 1‑based indexing
                    }

                    // Build output file name
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"section_{sectionIndex}_{startPage}-{endPage}.pdf");

                    // Save the split section
                    sectionDoc.Save(outputPath);
                    Console.WriteLine($"Saved section {sectionIndex}: {outputPath}");
                }

                sectionIndex++;
            }
        }

        Console.WriteLine("Batch splitting completed.");
    }
}