using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string configPath = "pages_to_delete.txt"; // one page number per line
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read and parse page numbers from the configuration file
        List<int> pagesToDelete = new List<int>();
        foreach (string line in File.ReadAllLines(configPath))
        {
            if (int.TryParse(line.Trim(), out int pageNum) && pageNum > 0)
                pagesToDelete.Add(pageNum);
        }

        if (pagesToDelete.Count == 0)
        {
            Console.WriteLine("No valid page numbers found in configuration.");
            return;
        }

        // Delete from highest to lowest to avoid index shifting
        pagesToDelete.Sort((a, b) => b.CompareTo(a));

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count; // 1‑based page count

            foreach (int pageNum in pagesToDelete)
            {
                if (pageNum <= totalPages)
                {
                    // Aspose.Pdf uses 1‑based indexing; delete the specified page
                    doc.Pages.Delete(pageNum);
                }
                else
                {
                    Console.WriteLine($"Page {pageNum} is out of range (1‑{totalPages}), skipped.");
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages deleted. Output saved to '{outputPath}'.");
    }
}