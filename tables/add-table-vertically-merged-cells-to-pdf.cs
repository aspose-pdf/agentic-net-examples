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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page page = doc.Pages[1];

            // Create a table with two columns.
            Table table = new Table
            {
                // Optional: set column widths (percentage of page width).
                ColumnWidths = "200 200"
            };

            // First row.
            Row row1 = table.Rows.Add();
            // First column cell.
            Cell cell11 = row1.Cells.Add("Row 1, Column 1");
            // Second column cell that will span two rows vertically.
            Cell cell12 = row1.Cells.Add("Merged Cell (RowSpan = 2)");
            cell12.RowSpan = 2; // Merge vertically across the next row.

            // Second row.
            Row row2 = table.Rows.Add();
            // Only the first column cell is needed; the second column is occupied by the merged cell above.
            Cell cell21 = row2.Cells.Add("Row 2, Column 1");

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF. No SaveOptions needed for PDF format.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with merged cells saved to '{outputPath}'.");
    }
}