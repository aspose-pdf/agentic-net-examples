using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_width.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column whose width is fixed at 500 points
            Table table = new Table
            {
                // ColumnWidths accepts a space‑separated list; a single value defines the table width
                ColumnWidths = "500",
                // Optional: add a thin border so the table is visible
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a row with one cell containing some text
            Row row = table.Rows.Add();
            row.Cells.Add("This table has a fixed width of 500 points.");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}