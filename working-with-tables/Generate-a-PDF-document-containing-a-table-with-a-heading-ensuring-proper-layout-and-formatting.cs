using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its layout
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "120 250 100",
                // Default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Default padding inside cells
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Default text style for cells
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12
                }
            };

            // ----- Heading row -----
            Row headerRow = table.Rows.Add();

            // Create header cells with text
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Description"));
            headerRow.Cells.Add(headerCell2);

            Cell headerCell3 = new Cell();
            headerCell3.Paragraphs.Add(new TextFragment("Price"));
            headerRow.Cells.Add(headerCell3);

            // Apply heading style (background and bold text)
            foreach (Cell hdrCell in headerRow.Cells)
            {
                hdrCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                // The Cell class does not expose a TextState property directly.
                // Instead, set the TextState on each Paragraph (or TextFragment) inside the cell.
                foreach (var paragraph in hdrCell.Paragraphs)
                {
                    if (paragraph is TextFragment tf)
                    {
                        // Modify the existing TextState instance instead of assigning a new one
                        tf.TextState.Font = FontRepository.FindFont("Helvetica");
                        tf.TextState.FontSize = 14;
                        tf.TextState.FontStyle = FontStyles.Bold;
                    }
                }
            }

            // ----- Data rows -----
            // Row 1
            Row row1 = table.Rows.Add();
            Cell r1c1 = new Cell();
            r1c1.Paragraphs.Add(new TextFragment("Widget A"));
            row1.Cells.Add(r1c1);

            Cell r1c2 = new Cell();
            r1c2.Paragraphs.Add(new TextFragment("High quality widget"));
            row1.Cells.Add(r1c2);

            Cell r1c3 = new Cell();
            r1c3.Paragraphs.Add(new TextFragment("$10.00"));
            row1.Cells.Add(r1c3);

            // Row 2
            Row row2 = table.Rows.Add();
            Cell r2c1 = new Cell();
            r2c1.Paragraphs.Add(new TextFragment("Widget B"));
            row2.Cells.Add(r2c1);

            Cell r2c2 = new Cell();
            r2c2.Paragraphs.Add(new TextFragment("Standard widget"));
            row2.Cells.Add(r2c2);

            Cell r2c3 = new Cell();
            r2c3.Paragraphs.Add(new TextFragment("$8.50"));
            row2.Cells.Add(r2c3);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
