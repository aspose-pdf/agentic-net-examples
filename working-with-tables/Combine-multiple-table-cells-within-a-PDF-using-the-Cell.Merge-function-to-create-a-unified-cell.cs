using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "merged_cells.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table 50 points from the left and 700 points from the bottom
                // (Aspose.Pdf uses bottom-left origin)
                // You can adjust Left and Top as needed
                // Left = 50,
                // Top = 700,
                // Set column widths (three columns, each 100 points wide)
                ColumnWidths = "100 100 100"
            };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -------------------------
            // First row – three cells
            // -------------------------
            Row row1 = table.Rows.Add();

            // Cell 1 – will be merged horizontally with Cell 2
            Cell cell1 = row1.Cells.Add("Cell 1-2 (merged)");
            // Set background color to visualize the merged area
            cell1.BackgroundColor = Color.LightGray;
            // Merge horizontally by spanning two columns
            cell1.ColSpan = 2;

            // Cell 3 – normal cell
            Cell cell3 = row1.Cells.Add("Cell 3");
            cell3.BackgroundColor = Color.AliceBlue;

            // -------------------------
            // Second row – three cells
            // -------------------------
            Row row2 = table.Rows.Add();

            // Cell 4 – will be merged vertically with Cell 7 (below)
            Cell cell4 = row2.Cells.Add("Cell 4-7 (merged vertically)");
            cell4.BackgroundColor = Color.LightGreen;
            // Merge vertically by spanning two rows
            cell4.RowSpan = 2;

            // Cell 5 – normal cell
            Cell cell5 = row2.Cells.Add("Cell 5");
            cell5.BackgroundColor = Color.LightCoral;

            // Cell 6 – normal cell
            Cell cell6 = row2.Cells.Add("Cell 6");
            cell6.BackgroundColor = Color.LightYellow;

            // -------------------------
            // Third row – three cells
            // -------------------------
            Row row3 = table.Rows.Add();

            // Cell 7 – part of the vertical merge started in Cell 4
            // Since Cell 4 already spans this row, we add a placeholder cell
            // (Aspose.Pdf requires a cell object, but it will be ignored in rendering)
            Cell placeholder = row3.Cells.Add();
            placeholder.IsNoBorder = true; // hide the placeholder

            // Cell 8 – normal cell
            Cell cell8 = row3.Cells.Add("Cell 8");
            cell8.BackgroundColor = Color.LightPink;

            // Cell 9 – normal cell
            Cell cell9 = row3.Cells.Add("Cell 9");
            cell9.BackgroundColor = Color.LightSkyBlue;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged cells saved to '{outputPath}'.");
    }
}