using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string configPath = "pages_to_delete.txt";

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

        // Read page numbers from the configuration file.
        // The file may contain one number per line or comma‑separated values.
        List<int> pagesToDelete = new List<int>();
        foreach (string line in File.ReadAllLines(configPath))
        {
            foreach (string part in line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(part.Trim(), out int pageNum) && pageNum > 0)
                    pagesToDelete.Add(pageNum);
            }
        }

        // Remove duplicates and sort descending to avoid index shift while deleting.
        pagesToDelete = pagesToDelete.Distinct().OrderByDescending(p => p).ToList();

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Delete each specified page (1‑based indexing).
            foreach (int pageNum in pagesToDelete)
            {
                if (pageNum <= pageCount)
                {
                    doc.Pages.Delete(pageNum);
                }
                else
                {
                    Console.WriteLine($"Warning: page {pageNum} does not exist (max {pageCount}).");
                }
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Pages deleted. Result saved to '{outputPdf}'.");
    }
}