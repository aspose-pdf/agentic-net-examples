using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare sample data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(int));

        dt.Rows.Add(1, "Alice", 85);
        dt.Rows.Add(2, "Bob", 72);
        dt.Rows.Add(3, "Charlie", 90);
        dt.Rows.Add(4, "Diana", 65);
        dt.Rows.Add(5, "Eve", 78);

        // Create a DataView and filter rows (e.g., only scores >= 80)
        DataView view = new DataView(dt);
        view.RowFilter = "Score >= 80";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                // Position the table 50 points from the left and 700 points from the bottom
                Left = 50,
                Top = 700,
                // Optional: set column widths (example: three equal columns)
                ColumnWidths = "100 100 100"
            };

            // Import the filtered DataView into the table
            // Parameters:
            //   sourceDataView          : view (already filtered)
            //   isColumnNamesImported  : true (include column headers)
            //   firstFilledRow          : 0 (start at first row of the table)
            //   firstFilledColumn       : 0 (start at first column)
            //   maxRows                 : view.Count (import all filtered rows)
            //   maxColumns              : view.Table.Columns.Count (import all columns)
            table.ImportDataView(view, true, 0, 0, view.Count, view.Table.Columns.Count);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("FilteredTable.pdf");
        }

        Console.WriteLine("PDF with filtered table saved as 'FilteredTable.pdf'.");
    }
}