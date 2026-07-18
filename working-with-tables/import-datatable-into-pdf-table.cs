using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare in‑memory data
        DataTable dt = new DataTable();
        dt.Columns.Add("Product", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add("Apple", 10, 0.5m);
        dt.Rows.Add("Banana", 5, 0.3m);
        dt.Rows.Add("Cherry", 20, 1.2m);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Optional: define column widths (three columns)
            table.ColumnWidths = "150 100 100";

            // Import the DataTable into the Aspose.Pdf.Table
            // - include column names as the first row
            // - start importing at row 0, column 0 (zero‑based)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the populated table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("DataTableExport.pdf");
        }
    }
}