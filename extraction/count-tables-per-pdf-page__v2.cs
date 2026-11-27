using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Mapping of page number (1‑based) to detected table count
            Dictionary<int, int> pageTableCounts = new Dictionary<int, int>();

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create a TableAbsorber and visit the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[pageNum]);

                // The number of tables found on the page is the count of the TableList collection
                int tableCount = absorber.TableList.Count;

                pageTableCounts[pageNum] = tableCount;
            }

            // Output the mapping
            foreach (var kvp in pageTableCounts)
            {
                Console.WriteLine($"Page {kvp.Key}: {kvp.Value} table(s) detected");
            }
        }
    }
}
