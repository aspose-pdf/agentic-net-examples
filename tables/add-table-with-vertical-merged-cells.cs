using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for any text handling if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and define column widths (optional)
            Table table = new Table
            {
                ColumnWidths = "100 150 150" // three columns with specified widths
            };

            // ---------- Row 1 ----------
            Row row1 = table.Rows.Add();
            // First cell will span vertically across three rows
            Cell mergedCell = row1.Cells.Add("Merged Cell");
            mergedCell.RowSpan = 3; // merge vertically across three rows
            // Add remaining cells for this row
            row1.Cells.Add("R1C2");
            row1.Cells.Add("R1C3");

            // ---------- Row 2 ----------
            Row row2 = table.Rows.Add();
            // No cell for the first column because it is merged
            row2.Cells.Add("R2C2");
            row2.Cells.Add("R2C3");

            // ---------- Row 3 ----------
            Row row3 = table.Rows.Add();
            // No cell for the first column because it is merged
            row3.Cells.Add("R3C2");
            row3.Cells.Add("R3C3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}