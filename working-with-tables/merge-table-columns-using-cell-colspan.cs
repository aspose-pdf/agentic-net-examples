using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with four equal-width columns
            Table table = new Table
            {
                // Column widths are specified in points; four columns of 100 points each
                ColumnWidths = "100 100 100 100",
                // Add a thin black border around the table
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a single row to the table
            Row row = table.Rows.Add();

            // Add a cell that will span three adjacent columns
            Cell spanningCell = row.Cells.Add("Spanning Cell");
            spanningCell.ColSpan = 3; // Merge three columns into this cell

            // Add the remaining cell for the fourth column
            row.Cells.Add("Normal Cell");

            // Save the resulting PDF
            doc.Save("TableWithColSpan.pdf");
        }
    }
}