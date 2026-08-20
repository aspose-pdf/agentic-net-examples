using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for ColumnAdjustment enum
using Aspose.Pdf.Text;   // for TextFragment

class Program
{
    static void Main()
    {
        // Prepare sample data in a DataTable
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Score", typeof(int));

        dataTable.Rows.Add(1, "Alice", 85);
        dataTable.Rows.Add(2, "Bob", 70);
        dataTable.Rows.Add(3, "Charlie", 90);
        dataTable.Rows.Add(4, "David", 60);

        // Create a DataView and filter rows (e.g., only scores >= 80)
        DataView filteredView = new DataView(dataTable);
        filteredView.RowFilter = "Score >= 80";

        // Create a new PDF document (lifecycle managed by using)
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a table that will hold the filtered data
            Table table = new Table();

            // Optional: make the table auto‑fit its content for a nicer layout
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // -----------------------------------------------------------------
            // Build the table manually – ImportDataView has a known bug that can
            // throw a NullReferenceException when a DataView with a filter is
            // used.  Populating the table row‑by‑row avoids that issue.
            // -----------------------------------------------------------------

            // Add header row
            Row header = table.Rows.Add();
            foreach (DataColumn col in dataTable.Columns)
            {
                Cell headerCell = header.Cells.Add();
                headerCell.Paragraphs.Add(new TextFragment(col.ColumnName));
            }

            // Add data rows from the filtered view
            foreach (DataRowView drv in filteredView)
            {
                Row dataRow = table.Rows.Add();
                foreach (DataColumn col in dataTable.Columns)
                {
                    Cell cell = dataRow.Cells.Add();
                    // Convert the value to string – handles nulls safely
                    string cellText = drv[col.ColumnName]?.ToString() ?? string.Empty;
                    cell.Paragraphs.Add(new TextFragment(cellText));
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            pdfDoc.Save("FilteredTable.pdf");
        }
    }
}
