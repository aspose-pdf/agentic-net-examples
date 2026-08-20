using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare sample data in a DataTable
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(double));

        dt.Rows.Add(1, "Alice", 85.5);
        dt.Rows.Add(2, "Bob", 92.0);
        dt.Rows.Add(3, "Charlie", 78.0);
        dt.Rows.Add(4, "Diana", 88.5);
        dt.Rows.Add(5, "Ethan", 91.0);

        // Create a DataView with a filter (e.g., Score >= 85)
        DataView view = new DataView(dt);
        view.RowFilter = "Score >= 85";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table and set basic appearance
            Table table = new Table
            {
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "100 200 100"
            };

            // Import the filtered DataView into the table
            // Parameters:
            //   sourceDataView: the DataView to import
            //   isColumnNamesImported: true to include column headers as first row
            //   firstFilledRow: 0 (start at first row of the table)
            //   firstFilledColumn: 0 (start at first column)
            //   maxRows: view.Count (import all filtered rows)
            //   maxColumns: view.Table.Columns.Count (import all columns)
            table.ImportDataView(view, true, 0, 0, view.Count, view.Table.Columns.Count);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("DataViewTable.pdf");
        }

        Console.WriteLine("PDF with imported DataView table saved as 'DataViewTable.pdf'.");
    }
}