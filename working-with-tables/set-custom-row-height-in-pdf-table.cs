using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_custom_row_height.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // First row – default height
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // Second row – custom fixed height (e.g., 100 points)
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Tall Cell 1");
            row2.Cells.Add("Tall Cell 2");
            row2.Cells.Add("Tall Cell 3");
            row2.FixedRowHeight = 100.0; // set fixed row height

            // Third row – default height
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Cell 4");
            row3.Cells.Add("Cell 5");
            row3.Cells.Add("Cell 6");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (explicit Save call)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}