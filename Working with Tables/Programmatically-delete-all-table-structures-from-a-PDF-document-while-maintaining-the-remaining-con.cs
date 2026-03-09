using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Create a TableAbsorber to locate tables on the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the page
                absorber.Visit(page);

                // Copy the found tables to a separate list because Remove() modifies TableList
                List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

                // Remove each table from the page
                foreach (AbsorbedTable tbl in tables)
                {
                    absorber.Remove(tbl);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All tables removed. Output saved to '{outputPath}'.");
    }
}