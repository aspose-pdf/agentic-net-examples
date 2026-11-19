using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect all Table objects present in the page's Paragraphs collection
                List<Table> tablesToRemove = new List<Table>();
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Table tbl)
                    {
                        tablesToRemove.Add(tbl);
                    }
                }

                // Remove each collected table from the Paragraphs collection
                foreach (Table tbl in tablesToRemove)
                {
                    page.Paragraphs.Remove(tbl);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed and saved to '{outputPath}'.");
    }
}