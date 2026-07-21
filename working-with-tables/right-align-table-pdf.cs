using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "right_aligned_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Align the table to the right margin
            table.Alignment = HorizontalAlignment.Right;

            // Adjust the left coordinate if needed; setting to 0 lets the alignment handle positioning
            table.Left = 0;

            // Optional: define column widths (three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add a single row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}