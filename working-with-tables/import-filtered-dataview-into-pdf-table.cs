using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare sample data
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Rows.Add(1, "Alice");
        dataTable.Rows.Add(2, "Bob");
        dataTable.Rows.Add(3, "Charlie");

        // Create a DataView with a filter (only rows where ID > 1)
        DataView dataView = new DataView(dataTable);
        dataView.RowFilter = "ID > 1";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and define column widths (optional)
            Table table = new Table();
            table.ColumnWidths = "100 200";

            // Import the filtered DataView into the table
            // Parameters:
            //   sourceDataView          : dataView
            //   isColumnNamesImported  : true (include column headers)
            //   firstFilledRow         : 0 (start at first row of the table)
            //   firstFilledColumn      : 0 (start at first column of the table)
            //   maxRows                : dataView.Count (import all filtered rows)
            //   maxColumns             : dataView.Table.Columns.Count (import all columns)
            table.ImportDataView(dataView, true, 0, 0, dataView.Count, dataView.Table.Columns.Count);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("DataViewTable.pdf");
        }
    }
}