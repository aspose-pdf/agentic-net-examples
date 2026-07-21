using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_custom_row_height.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns (widths in points)
            Table table = new Table
            {
                ColumnWidths = "100 200 100"
            };
            page.Paragraphs.Add(table);

            // ----- Row 1: default height -----
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // ----- Row 2: custom fixed height (e.g., 50 points) -----
            Row row2 = table.Rows.Add();
            row2.FixedRowHeight = 50; // sets a fixed row height in points
            row2.Cells.Add("Tall Cell 1");
            row2.Cells.Add("Tall Cell 2");
            row2.Cells.Add("Tall Cell 3");

            // ----- Row 3: default height -----
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Cell 4");
            row3.Cells.Add("Cell 5");
            row3.Cells.Add("Cell 6");

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}