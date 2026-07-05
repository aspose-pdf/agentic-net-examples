using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_rowspan.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // First row
            Row row1 = table.Rows.Add();

            // Cell that will span two rows (merge with the cell directly below)
            Cell spanningCell = row1.Cells.Add("Spanning Cell");
            spanningCell.RowSpan = 2; // Set RowSpan to merge with the next row

            // Remaining cells in the first row
            row1.Cells.Add("R1C2");
            row1.Cells.Add("R1C3");

            // Second row (the first column is omitted because of the rowspan)
            Row row2 = table.Rows.Add();
            row2.Cells.Add("R2C2");
            row2.Cells.Add("R2C3");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}