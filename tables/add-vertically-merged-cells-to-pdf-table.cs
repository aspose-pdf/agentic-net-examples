using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a new table.
            Table table = new Table();

            // Define column widths (optional, here three equal columns).
            table.ColumnWidths = "100 100 100";

            // Add the first row.
            Row row1 = table.Rows.Add();

            // Add a cell that will span two rows vertically.
            Cell mergedCell = row1.Cells.Add("Merged Cell");
            mergedCell.RowSpan = 2; // Merge this cell across the next row.

            // Add remaining cells for the first row.
            row1.Cells.Add("R1C2");
            row1.Cells.Add("R1C3");

            // Add the second row.
            Row row2 = table.Rows.Add();

            // Since the first cell of row2 is merged, we only add cells for the remaining columns.
            row2.Cells.Add("R2C2");
            row2.Cells.Add("R2C3");

            // Insert the table into the first page's paragraph collection.
            // Pages are 1‑based indexed.
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with merged cells saved to '{outputPath}'.");
    }
}