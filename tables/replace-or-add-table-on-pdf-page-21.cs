using System;
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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 21 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 21)
            {
                Console.Error.WriteLine("The document does not contain page 21.");
                return;
            }

            // Create a new table
            Table table = new Table();

            // Set column widths (comma‑ or space‑separated string; values are in points)
            table.ColumnWidths = "100 150 200";

            // Add a row and populate its cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Access page 21 (1‑based index)
            Page page = doc.Pages[21];

            // Try to locate an existing table on the page
            int tableIndex = -1;
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                if (page.Paragraphs[i] is Table)
                {
                    tableIndex = i;
                    break;
                }
            }

            if (tableIndex >= 0)
            {
                // Replace the existing table with the new one
                page.Paragraphs[tableIndex] = table;
            }
            else
            {
                // No existing table found; add the new table to the page
                page.Paragraphs.Add(table);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
