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
            // Ensure the document has at least 19 pages.
            if (doc.Pages.Count < 19)
            {
                Console.Error.WriteLine("The document does not contain page 19.");
                return;
            }

            // Get page 19 (Aspose.Pdf uses 1‑based indexing).
            Page page19 = doc.Pages[19];

            // Create a new table.
            Table newTable = new Table();

            // Set a default cell border (all sides, 0.5 point width).
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add a row with three cells.
            Row row = newTable.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Try to locate an existing table on the page.
            Table existingTable = null;
            int existingIndex = -1;
            for (int i = 0; i < page19.Paragraphs.Count; i++)
            {
                if (page19.Paragraphs[i] is Table tbl)
                {
                    existingTable = tbl;
                    existingIndex = i;
                    break;
                }
            }

            if (existingTable != null && existingIndex >= 0)
            {
                // Replace the found table with the new one at the same position.
                page19.Paragraphs[existingIndex] = newTable;
            }
            else
            {
                // No existing table found; simply add the new table to the page.
                page19.Paragraphs.Add(newTable);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}