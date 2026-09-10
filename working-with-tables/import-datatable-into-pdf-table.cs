using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare an in‑memory DataTable
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Rows.Add(1, "Alice");
        dt.Rows.Add(2, "Bob");
        dt.Rows.Add(3, "Charlie");

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and optionally define column widths
            Table table = new Table();
            table.ColumnWidths = "100 200";

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: DataTable, import column names as first row, start at row 0, column 0
            table.ImportDataTable(dt, true, 0, 0);

            // Add the populated table to the page
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save("DataTableExport.pdf");
        }
    }
}