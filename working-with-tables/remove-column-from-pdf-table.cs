using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // not used, just placeholder
        const string outputPath = "output.pdf";

        // Create a new PDF document and add a page
        var doc = new Document();
        var page = doc.Pages.Add();

        // -----------------------------------------------------------------
        // Build a sample table (3 columns, 2 rows) to demonstrate removal
        // -----------------------------------------------------------------
        var table = new Table
        {
            // Define three equal column widths (you can adjust as needed)
            ColumnWidths = "120 120 120",
            // Optional visual styling
            Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
            DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
        };

        // Header row
        var header = table.Rows.Add();
        header.Cells.Add("Col 1");
        header.Cells.Add("Col 2");
        header.Cells.Add("Col 3");

        // Body row
        var body = table.Rows.Add();
        body.Cells.Add("A1");
        body.Cells.Add("B1");
        body.Cells.Add("C1");

        // ---------------------------------------------------------------
        // Remove a specific column (e.g., column 2) from the table
        // ---------------------------------------------------------------
        int columnToRemove = 2; // 1‑based column number
        int zeroBasedIndex = columnToRemove - 1;

        // Remove the cell at the given index from every row
        foreach (Row row in table.Rows)
        {
            if (zeroBasedIndex >= 0 && zeroBasedIndex < row.Cells.Count)
                row.Cells.Remove(row.Cells[zeroBasedIndex]);
        }

        // Adjust the ColumnWidths string to match the new column count
        // (simply remove the corresponding width entry)
        var widths = table.ColumnWidths.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (zeroBasedIndex >= 0 && zeroBasedIndex < widths.Length)
        {
            var newWidths = string.Join(" ", widths, 0, zeroBasedIndex) +
                            (zeroBasedIndex + 1 < widths.Length ? " " + string.Join(" ", widths, zeroBasedIndex + 1, widths.Length - zeroBasedIndex - 1) : string.Empty);
            table.ColumnWidths = newWidths.Trim();
        }

        // Add the table to the page
        page.Paragraphs.Add(table);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
