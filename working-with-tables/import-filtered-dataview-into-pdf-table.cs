using System;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare sample data
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Score", typeof(double));

        dataTable.Rows.Add(1, "Alice", 85.5);
        dataTable.Rows.Add(2, "Bob", 92.0);
        dataTable.Rows.Add(3, "Charlie", 78.0);
        dataTable.Rows.Add(4, "David", 88.5);

        // Create a DataView with a filter (only rows with Score >= 80)
        DataView view = new DataView(dataTable);
        view.RowFilter = "Score >= 80";

        // Create PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a Table layout element
            Table table = new Table();

            // Safely obtain row and column counts (null‑guarded)
            int rowsCount = view?.Count ?? 0;
            int columnsCount = view?.Table?.Columns?.Count ?? 0;

            if (rowsCount > 0 && columnsCount > 0)
            {
                // Define column widths before importing data. Using a simple equal width for each column.
                // This step prevents a NullReferenceException inside ImportDataView.
                table.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", columnsCount));

                // Parameters: (DataView view, bool isColumnNamesImported, int startRow, int startColumn, int rowsCount, int columnsCount)
                table.ImportDataView(view, true, 0, 0, rowsCount, columnsCount);
            }
            else
            {
                // Optional: add a placeholder row when the view is empty
                table.ColumnWidths = "100 150 100"; // simple widths for three columns
                table.Rows.Add(new Row());
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("DataViewTable.pdf");
        }
    }
}
