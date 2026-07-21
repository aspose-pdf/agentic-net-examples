using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string configPath = "ranges.txt";    // each line: start-end (1‑based)
        const string outputDir  = "Sections";     // folder for split PDFs

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

        Directory.CreateDirectory(outputDir);

        // Read page ranges from the configuration file
        string[] lines = File.ReadAllLines(configPath);
        using (Document source = new Document(inputPdf)) // load source PDF
        {
            int sectionNumber = 1;

            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue; // skip empty lines

                // Expected format: "start-end"
                string[] parts = rawLine.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2 ||
                    !int.TryParse(parts[0].Trim(), out int startPage) ||
                    !int.TryParse(parts[1].Trim(), out int endPage))
                {
                    Console.Error.WriteLine($"Invalid range line: '{rawLine}'");
                    continue;
                }

                // Validate range against the source document page count (1‑based indexing)
                if (startPage < 1 || endPage > source.Pages.Count || startPage > endPage)
                {
                    Console.Error.WriteLine($"Range out of bounds: {startPage}-{endPage}");
                    continue;
                }

                // Create a new document for this section and copy the required pages
                using (Document section = new Document())
                {
                    for (int i = startPage; i <= endPage; i++)
                    {
                        // Add copies of the pages from the source document
                        section.Pages.Add(source.Pages[i]); // Pages are 1‑based
                    }

                    string outPath = Path.Combine(outputDir, $"section_{sectionNumber}.pdf");
                    section.Save(outPath); // save the split section as PDF
                    Console.WriteLine($"Saved section {sectionNumber}: {outPath}");
                }

                sectionNumber++;
            }
        }
    }
}