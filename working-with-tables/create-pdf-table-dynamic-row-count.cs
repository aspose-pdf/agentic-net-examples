using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare source data (DataTable) with a dynamic number of rows
        DataTable sourceTable = BuildSourceData();

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document (Pages collection is 1‑based)
            Page page = pdfDoc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Example: set column widths (optional, adjust as needed)
            table.ColumnWidths = "100 150 100";

            // Import the DataTable into the Aspose.Pdf.Table
            // - true  => import column names as the first row
            // - 0, 0  => start importing at the first row and first column of the table
            table.ImportDataTable(sourceTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            pdfDoc.Save("output.pdf");
        }
    }

    // Builds a DataTable with a dynamic row count determined at runtime
    static DataTable BuildSourceData()
    {
        DataTable dt = new DataTable();

        // Define columns
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Amount", typeof(double));

        // Determine the number of records dynamically (e.g., based on external logic)
        int recordCount = GetRecordCount();

        // Populate the DataTable
        for (int i = 1; i <= recordCount; i++)
        {
            dt.Rows.Add(i, $"Item {i}", i * 12.34);
        }

        return dt;
    }

    // Placeholder for logic that determines how many records to import
    static int GetRecordCount()
    {
        // In a real scenario this could query a database, read a file, etc.
        // Here we simply return a fixed number for demonstration.
        return 7;
    }
}