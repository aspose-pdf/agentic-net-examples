using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_cell.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            // Define column width (Cell.Width is read‑only, use Table.ColumnWidths instead)
            table.ColumnWidths = "200"; // width in points for the single column
            page.Paragraphs.Add(table);

            // Add a single row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a TextFragment, set its rotation, and add it to the cell
            TextFragment tf = new TextFragment("Rotated Text");
            tf.TextState.Rotation = 45; // rotates the text 45 degrees
            cell.Paragraphs.Add(tf);

            // Optional: set row height if needed (Cell height is controlled via Row)
            // row.FixedRowHeight = 50;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
