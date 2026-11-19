using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a DataTable with sample data
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Score", typeof(double));

        dataTable.Rows.Add(1, "Alice", 85.5);
        dataTable.Rows.Add(2, "Bob", 92.0);
        dataTable.Rows.Add(3, "Charlie", 78.0);
        dataTable.Rows.Add(4, "Diana", 88.5);

        // Create a DataView that filters rows where Score >= 80
        DataView view = new DataView(dataTable);
        view.RowFilter = "Score >= 80";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set column widths (optional)
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.ColumnWidths = "100 200 100";

            // Determine the range where the data will be placed in the table
            // Rows and columns are 1‑based for ImportDataView.
            int firstFilledRow = 1;               // start at the first row of the table
            int firstFilledColumn = 1;            // start at the first column of the table
            int totalRows = view.Count;           // number of data rows after filtering
            int totalColumns = view.Table.Columns.Count; // number of columns in the source table
            int lastFilledRow = totalRows + 1;    // +1 to account for the header row when includeColumnHeaders = true
            int lastFilledColumn = totalColumns; // fill all columns

            // Import the filtered DataView into the table, including column headers
            table.ImportDataView(view, true, firstFilledRow, firstFilledColumn, lastFilledRow, lastFilledColumn);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("TableFromDataView.pdf");
        }

        Console.WriteLine("PDF with imported DataView table created successfully.");
    }
}
