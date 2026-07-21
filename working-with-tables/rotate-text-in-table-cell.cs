using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateCellTextExample
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table();
            // ColumnWidths is a string property – set it directly (comma‑separated for multiple columns)
            table.ColumnWidths = "200"; // set column width

            // Add a row to the table
            Row row = table.Rows.Add();
            // Optional: control row height if needed
            // row.FixedRowHeight = 100; // or row.MinRowHeight = 50;

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a TextFragment and rotate its text
            TextFragment tf = new TextFragment("Rotated Text");
            tf.TextState.Rotation = 45; // angle in degrees (0‑360)

            // Add the rotated text fragment to the cell
            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save("RotatedCellText.pdf");
        }

        Console.WriteLine("PDF with rotated cell text saved as 'RotatedCellText.pdf'.");
    }
}