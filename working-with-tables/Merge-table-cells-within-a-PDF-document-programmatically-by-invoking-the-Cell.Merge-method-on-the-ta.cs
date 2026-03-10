using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "merged_cells.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                // Define three columns of equal width
                ColumnWidths = "120 120 120",
                // Optional: set a thin border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                // Optional: add some padding inside cells
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 }
            };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // ---------- First row ----------
            Row firstRow = table.Rows.Add();

            // Add three cells to the first row
            Cell cellA = firstRow.Cells.Add("Cell A");
            Cell cellB = firstRow.Cells.Add("Cell B");
            Cell cellC = firstRow.Cells.Add("Cell C");

            // Merge the first two cells horizontally.
            // In Aspose.Pdf merging is achieved by setting the ColSpan property
            // on the leftmost cell to the number of columns to span.
            cellA.ColSpan = 2; // spans columns A and B

            // The second cell (cellB) remains in the collection but its content
            // will be ignored because column A now occupies its space.

            // ---------- Second row ----------
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add("Row 2, Col 1");
            secondRow.Cells.Add("Row 2, Col 2");
            secondRow.Cells.Add("Row 2, Col 3");

            // Save the document. No SaveOptions are needed because the target
            // format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged cells saved to '{outputPath}'.");
    }
}