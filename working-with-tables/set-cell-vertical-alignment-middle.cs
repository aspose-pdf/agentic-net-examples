using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns and a thin border
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            Cell headerCell3 = header.Cells.Add("Header 3");

            // Set vertical and horizontal alignment for header cells
            SetCellAlignment(headerCell1);
            SetCellAlignment(headerCell2);
            SetCellAlignment(headerCell3);

            // Add three data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                Cell c1 = row.Cells.Add($"R{i}C1");
                Cell c2 = row.Cells.Add($"R{i}C2");
                Cell c3 = row.Cells.Add($"R{i}C3");

                // Set vertical and horizontal alignment for each data cell
                SetCellAlignment(c1);
                SetCellAlignment(c2);
                SetCellAlignment(c3);
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Applies vertical centre alignment and horizontal centre alignment to a cell.
    /// Horizontal alignment is set via the cell's DefaultCellTextState because the Cell class
    /// does not expose a direct HorizontalAlignment property.
    /// </summary>
    private static void SetCellAlignment(Cell cell)
    {
        // Vertical alignment is directly supported on the Cell class.
        cell.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;

        // Horizontal alignment must be defined through the TextState associated with the cell.
        cell.DefaultCellTextState = new TextState
        {
            HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center
        };
    }
}
