using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_table.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Rotate the entire page 90 degrees clockwise.
            // This applies a transformation matrix to the page before any content (including the table) is added.
            page.Rotate = Rotation.on90;

            // Create a table and populate it with sample data
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points)
                // Since the page is rotated, these coordinates are interpreted in the rotated space.
                Left = 50,
                Top = 50,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black),
                // Define column widths as a space‑separated string (ColumnWidths is a string property in recent Aspose.Pdf versions)
                ColumnWidths = "100 100 100"
            };

            // Add a header row
            Row header = table.Rows.Add();

            // Helper method to create a cell with bold text
            Cell CreateHeaderCell(string text)
            {
                var tf = new TextFragment(text);
                tf.TextState.FontStyle = FontStyles.Bold;
                var cell = new Cell();
                cell.Paragraphs.Add(tf);
                return cell;
            }

            header.Cells.Add(CreateHeaderCell("Header 1"));
            header.Cells.Add(CreateHeaderCell("Header 2"));
            header.Cells.Add(CreateHeaderCell("Header 3"));

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();

                Cell c1 = new Cell();
                c1.Paragraphs.Add(new TextFragment($"Row {i} - Col 1"));
                row.Cells.Add(c1);

                Cell c2 = new Cell();
                c2.Paragraphs.Add(new TextFragment($"Row {i} - Col 2"));
                row.Cells.Add(c2);

                Cell c3 = new Cell();
                c3.Paragraphs.Add(new TextFragment($"Row {i} - Col 3"));
                row.Cells.Add(c3);
            }

            // Add the table to the page's paragraph collection.
            // Because the page rotation has already been set, the table will be rendered rotated.
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated table saved to '{outputPath}'.");
    }
}
