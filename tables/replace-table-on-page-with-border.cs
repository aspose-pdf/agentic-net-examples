using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Create a new Table with a default cell border.
            // ------------------------------------------------------------
            Table newTable = new Table();

            // Set a thin border (0.5 points) on all sides of each cell.
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add a first row with two cells.
            Row row1 = newTable.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");

            // Add a second row with two cells.
            Row row2 = newTable.Rows.Add();
            row2.Cells.Add("Data A");
            row2.Cells.Add("Data B");

            // ------------------------------------------------------------
            // Replace the existing table on page 8 (1‑based indexing).
            // ------------------------------------------------------------
            // Ensure the document has at least 8 pages.
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            Page pageEight = doc.Pages[8]; // 1‑based index per global rule.

            // Find the first Table element in the page's paragraphs collection.
            int tableIndex = -1;
            for (int i = 0; i < pageEight.Paragraphs.Count; i++)
            {
                if (pageEight.Paragraphs[i] is Table)
                {
                    tableIndex = i;
                    break;
                }
            }

            if (tableIndex >= 0)
            {
                // Replace the existing table with the new one.
                pageEight.Paragraphs[tableIndex] = newTable;
            }
            else
            {
                // No existing table found; add the new table at the end of the page.
                pageEight.Paragraphs.Add(newTable);
            }

            // ------------------------------------------------------------
            // Save the modified PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
