using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare an in‑memory DataTable with sample data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));
        dt.Rows.Add(1, "Apple", 0.5m);
        dt.Rows.Add(2, "Banana", 0.3m);
        dt.Rows.Add(3, "Cherry", 1.2m);

        // Create a PDF document (lifecycle managed by using)
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Instantiate a Table and optionally set column widths
            Table table = new Table
            {
                ColumnWidths = "100 200 100"
            };

            // Import the DataTable into the Aspose.Pdf Table
            // Parameters: source DataTable, import column names as first row,
            // start at row index 0, column index 0 in the target table
            table.ImportDataTable(dt, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save("DataTableExport.pdf");
        }
    }
}