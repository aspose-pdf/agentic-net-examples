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
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table with five equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell that will span three adjacent columns
            Cell spanningCell = row.Cells.Add("Spanning Cell");
            spanningCell.ColSpan = 3; // Merge three columns into this cell
            spanningCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            spanningCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Add the remaining cells to complete the row (columns 4 and 5)
            row.Cells.Add("Cell 4");
            row.Cells.Add("Cell 5");

            // Save the PDF to disk
            doc.Save("TableWithColSpan.pdf");
        }
    }
}