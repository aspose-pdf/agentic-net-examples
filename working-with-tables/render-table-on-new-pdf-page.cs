using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes (Document, Page, Table, Row, Cell)

class RenderTableExample
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RenderedTable.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Table object (inherits BaseParagraph, can be added to page.Paragraphs)
            Table table = new Table();

            // Optional: set table appearance
            table.Border = new BorderInfo(BorderSide.All, 1f);               // thin border around the table
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);   // thin border for each cell
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);          // padding inside cells

            // Define three columns (optional – widths are auto‑calculated if not set)
            // table.ColumnWidths.Add(100); // Uncomment to set explicit column widths

            // Add header row
            Row header = table.Rows.Add();
            // Mark header visually (Row has no IsHeader property in recent versions)
            header.BackgroundColor = Color.LightGray;
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Add a few data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("10");
            row1.Cells.Add("$15.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("5");
            row2.Cells.Add("$25.00");

            Row row3 = table.Rows.Add();
            row3.Cells.Add("Widget C");
            row3.Cells.Add("12");
            row3.Cells.Add("$9.99");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table rendered and saved to '{Path.GetFullPath(outputPath)}'.");
    }
}
