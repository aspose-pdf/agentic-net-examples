using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_no_split.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table and disable automatic splitting across pages
            Table table = new Table
            {
                IsBroken = false,                 // Force the table to stay on one page
                ColumnWidths = "100 100 100"      // Simple column width definition
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add many rows to exceed a page height (if splitting were allowed)
            for (int i = 0; i < 50; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i + 1} Col 1");
                row.Cells.Add($"Row {i + 1} Col 2");
                row.Cells.Add($"Row {i + 1} Col 3");
            }

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Table will not split across pages.");
    }
}