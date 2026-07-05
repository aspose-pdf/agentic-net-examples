using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample data creation (replace with actual data source)
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(int));

        dt.Rows.Add(1, "Alice", 85);
        dt.Rows.Add(2, "Bob", 72);
        dt.Rows.Add(3, "Charlie", 90);
        dt.Rows.Add(4, "Diana", 65);

        // Create a DataView and filter rows (e.g., only scores >= 80)
        DataView view = new DataView(dt);
        view.RowFilter = "Score >= 80";

        // Create a new PDF document
        using (Document pdf = new Document())
        {
            // Add a page to the document
            Page page = pdf.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                // Use a valid ColumnAdjustment enum value
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };
            table.ColumnWidths = "100 200 100"; // Adjust column widths as needed
            table.Rows.Add(); // Ensure at least one row exists for column headers

            // Optional: add header row
            Row header = table.Rows[0]; // first row is the header after the Add()
            header.Cells.Add("Id");
            header.Cells.Add("Name");
            header.Cells.Add("Score");
            // Apply simple styling to header cells
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.White,
                    BackgroundColor = Color.Gray
                };
            }

            // Import filtered DataView into the table
            // Parameters:
            //   view               – source DataView
            //   false              – do NOT import column names (already added)
            //   1                  – start importing at row index 1 (second row, after header)
            //   0                  – start at first column
            //   int.MaxValue       – import all rows from the view
            //   int.MaxValue       – import all columns from the view
            table.ImportDataView(view, false, 1, 0, int.MaxValue, int.MaxValue);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            pdf.Save("FilteredTable.pdf");
        }

        Console.WriteLine("PDF with filtered table created successfully.");
    }
}
