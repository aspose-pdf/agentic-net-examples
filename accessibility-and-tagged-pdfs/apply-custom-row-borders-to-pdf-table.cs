using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set column widths
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };
            page.Paragraphs.Add(table);

            int rowCount = 6;
            int colCount = 3;

            // Populate the table
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // Define a border style based on the row index using the BorderInfo constructor
                BorderInfo rowBorder;
                if (rowIndex % 2 == 0) // even rows
                {
                    rowBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
                }
                else // odd rows
                {
                    rowBorder = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Red);
                }

                // Apply the border to the entire row
                row.Border = rowBorder;

                // Add cells to the row
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    Cell cell = new Cell
                    {
                        // Apply the same border to each cell (optional)
                        Border = rowBorder
                    };

                    // Add some sample text to the cell
                    cell.Paragraphs.Add(new TextFragment($"R{rowIndex + 1}C{colIndex + 1}"));
                    row.Cells.Add(cell);
                }
            }

            // Save the PDF document
            doc.Save("TableWithCustomBorders.pdf");
        }
    }
}
